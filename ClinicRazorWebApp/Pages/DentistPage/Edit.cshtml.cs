using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicData.Models;
using ClinicBusiness;
using ClinicCommon;
using Microsoft.AspNetCore.Http;

namespace ClinicRazorWebApp.Pages.Dentists
{
    public class EditDentistModel : PageModel
    {
        private readonly IDentistBusiness _dentistBusiness;
        private readonly IClinicBusinessClass _clinicBusiness;
        private readonly IUserBusiness _userBusiness;
        private readonly ICommonService _commonService;

        [BindProperty]
        public Dentist Dentist { get; set; } = default!;

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public string MaxDate { get; set; }

        public EditDentistModel(
            IDentistBusiness dentistBusiness,
            IClinicBusinessClass clinicBusiness,
            IUserBusiness userBusiness,
            ICommonService commonService)
        {
            _dentistBusiness = dentistBusiness;
            _clinicBusiness = clinicBusiness;
            _userBusiness = userBusiness;
            _commonService = commonService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dentistResponse = await _dentistBusiness.GetById(id.ToString());
            if (dentistResponse == null || dentistResponse.Data == null)
            {
                return NotFound();
            }

            Dentist = dentistResponse.Data as Dentist;
            if (Dentist == null)
            {
                return NotFound();
            }

            await LoadSelectListsAsync();
            MaxDate = DateTime.Today.AddYears(-24).ToString("yyyy-MM-dd");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadSelectListsAsync();

            bool isValid = true;

            // Validating phone number
            if (!Validation.IsValidPhoneNumber(Dentist.Phone))
            {
                ModelState.AddModelError("ERROR", "Phone is not valid");
                isValid = false;
            }
            // Validating email
            if (!Validation.IsValidEmail(Dentist.Email))
            {
                ModelState.AddModelError("ERROR", "Email is not valid");
                isValid = false;
            }
            if (!isValid)
            {
                return Page();
            }

            try
            {
                if (ImageFile != null)
                {
                    var imageUrl = await _commonService.UploadAnImage(ImageFile, "dentists", Dentist.DentistName);
                    Dentist.Image = imageUrl;
                }

                var updateResult = await _dentistBusiness.Update(Dentist);
                if (updateResult.Status != Const.SUCCESS_UPDATE_CODE)
                {
                    ModelState.AddModelError(string.Empty, "Error updating Dentist: " + updateResult.Message);
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred: " + ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private async Task LoadSelectListsAsync()
        {
            var clinicResult = await _clinicBusiness.GetAll();
            if (clinicResult.Status == Const.SUCCESS_READ_CODE)
            {
                var clinics = clinicResult.Data as List<Clinic>;
                if (clinics != null)
                {
                    ViewData["ClinicId"] = new SelectList(clinics, "ClinicId", "ClinicName");
                }
            }

            var userResult = await _userBusiness.GetAll();
            if (userResult.Status == Const.SUCCESS_READ_CODE)
            {
                var users = userResult.Data as List<User>;
                if (users != null)
                {
                    ViewData["UserId"] = new SelectList(users, "UserId", "Fullname");
                }
            }
        }
    }
}
