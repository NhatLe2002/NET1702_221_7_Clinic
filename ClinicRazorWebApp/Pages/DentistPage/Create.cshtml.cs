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

namespace ClinicRazorWebApp.Pages.DentistPage
{
    public class CreateModel : PageModel
    {
        private readonly IDentistBusiness _dentistBusiness;
        private readonly IClinicBusinessClass _clinicBusiness;
        private readonly IUserBusiness _userBusiness;
        private readonly ICommonService _commonService;

        [BindProperty]
        public Dentist Dentist { get; set; } = default!;
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public List<Clinic> Clinics { get; set; } = new List<Clinic>();
        public List<User> Users { get; set; } = new List<User>();

        public CreateModel(IDentistBusiness dentistBusiness, IClinicBusinessClass clinicBusiness, IUserBusiness userBusiness, ICommonService commonService)
        {
            _dentistBusiness = dentistBusiness;
            _clinicBusiness = clinicBusiness;
            _userBusiness = userBusiness;
            _commonService = commonService;
        }

        public async Task<IActionResult> OnGet()
        {
            var clinicResult = await _clinicBusiness.GetAll();
            if (clinicResult.Status != Const.SUCCESS_READ_CODE)
            {
                return Page();
            }

            var clinics = clinicResult.Data as List<Clinic>;
            if (clinics == null)
            {
                return Page();
            }

            ViewData["ClinicId"] = new SelectList(clinics.Select(clinic => new SelectListItem
            {
                Value = clinic.ClinicId.ToString(),
                Text = clinic.ClinicName
            }), "Value", "Text");

            var userResult = await _userBusiness.GetAll();
            if (userResult.Status != Const.SUCCESS_READ_CODE)
            {
                return Page();
            }

            var users = userResult.Data as List<User>;
            if (users == null)
            {
                return Page();
            }

            ViewData["UserId"] = new SelectList(users.Select(user => new SelectListItem
            {
                Value = user.UserId.ToString(),
                Text = user.Fullname
            }), "Value", "Text");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var clinicResult = await _clinicBusiness.GetAll();
                if (clinicResult == null || clinicResult.Status != Const.SUCCESS_READ_CODE)
                {
                    return Page();
                }

                var clinics = clinicResult.Data as List<Clinic>;
                if (clinics == null)
                {
                    return Page();
                }

                ViewData["ClinicId"] = new SelectList(clinics.Select(clinic => new SelectListItem
                {
                    Value = clinic.ClinicId.ToString(),
                    Text = clinic.ClinicName
                }), "Value", "Text");

                var userResult = await _userBusiness.GetAll();
                if (userResult == null || userResult.Status != Const.SUCCESS_READ_CODE)
                {
                    return Page();
                }

                var users = userResult.Data as List<User>;
                if (users == null)
                {
                    return Page();
                }

                ViewData["UserId"] = new SelectList(users.Select(user => new SelectListItem
                {
                    Value = user.UserId.ToString(),
                    Text = user.Fullname
                }), "Value", "Text");

                //return Page();
            }

            try
            {
                if (ImageFile != null)
                {
                    var imageUrl = await _commonService.UploadAnImage(ImageFile, "dentists", Dentist.DentistName);
                    Dentist.Image = imageUrl;
                }

                var dentistResult = await _dentistBusiness.Save(Dentist);
                if (dentistResult.Status != Const.SUCCESS_CREATE_CODE)
                {
                    ModelState.AddModelError(string.Empty, "Error creating Dentist: " + dentistResult.Message);
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
    }
}
