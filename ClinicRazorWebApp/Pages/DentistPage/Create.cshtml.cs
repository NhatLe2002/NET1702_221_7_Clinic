
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicData.Models;
using ClinicBusiness;
using DentistBusiness;
using ClinicCommon;

namespace ClinicRazorWebApp.Pages.DentistPage
{
    public class CreateModel : PageModel
    {
        private readonly IDentistBusiness _dentistBusiness;
        private readonly IClinicBusinessClass _clinicBusiness;
        private readonly IUserBusiness _userBusiness;

        [BindProperty]
        public Dentist Dentist { get; set; } = default!;
        public List<Clinic> Clinics { get; set; } = new List<Clinic>();
        public CreateModel(IDentistBusiness dentistBusiness, IClinicBusinessClass clinicBusiness, IUserBusiness userBusiness)
        {
            _dentistBusiness = dentistBusiness;
            _clinicBusiness = clinicBusiness;
            _userBusiness = userBusiness;
        }

        public async Task<IActionResult> OnGet()
        {
            var result = await _clinicBusiness.GetAll();
            if (result.Status != Const.SUCCESS_READ_CODE)
            {
                // Xử lý lỗi nếu cần
                return Page();
            }

            var clinicResult = result.Data as List<Clinic>;
            if (clinicResult == null)
            {
                // Xử lý lỗi nếu dữ liệu không hợp lệ
                return Page();
            }

            var clinics = clinicResult.Select(clinic => new SelectListItem
            {
                Value = clinic.ClinicId.ToString(),
                Text = clinic.ClinicName
            }).ToList();

            ViewData["ClinicId"] = new SelectList(clinics, "Value", "Text");

            return Page();
        }




        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Tải lại danh sách clinic nếu có lỗi validate
                var result = await _clinicBusiness.GetAll();
                if (result == null || result.Status != Const.SUCCESS_READ_CODE)
                {
                    return Page();
                }

                var clinicResult = result.Data as List<Clinic>;
                if (clinicResult == null)
                {
                    return Page();
                }

                ViewData["ClinicId"] = new SelectList(clinicResult.Select(clinic => new SelectListItem
                {
                    Value = clinic.ClinicId.ToString(),
                    Text = clinic.ClinicName
                }), "Value", "Text");

                return Page(); // Trả về Page để hiển thị lại form với các lỗi validation
            }

            try
            {
                var newUser = new User
                {
                    RoleId = 2,
                    Username = Dentist.Email,
                    Password = "123456",
                    IsActive = true,
                    Fullname = Dentist.DentistName,
                    Email = Dentist.Email,
                    Phone = Dentist.Phone,
                    Address = Dentist.Address,
                    Birthday = Dentist.DateOfBirth
                };

                var userResult = await _userBusiness.Save(newUser);
                if (userResult.Status != Const.SUCCESS_CREATE_CODE)
                {
                    ModelState.AddModelError(string.Empty, "Error creating User: " + userResult.Message);
                    return Page();
                }

                var createdUser = userResult.Data as User;
                if (createdUser != null && createdUser.UserId > 0)
                {
                    int newUserId = createdUser.UserId;
                    Dentist.UserId = newUserId;

                    var dentistResult = await _dentistBusiness.Save(Dentist);
                    if (dentistResult.Status != Const.SUCCESS_CREATE_CODE)
                    {
                        ModelState.AddModelError(string.Empty, "Error creating Dentist: " + dentistResult.Message);
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error creating User: Invalid user data returned");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred: " + ex.Message);
                return Page();
            }

            return RedirectToPage("./Index"); // Chỉ khi hoàn thành thành công mới chuyển hướng
        }

    }
}