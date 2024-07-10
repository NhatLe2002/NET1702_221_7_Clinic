using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;
using DentistBusiness;
using ClinicCommon;

namespace ClinicRazorWebApp.Pages.DentistPage
{
    public class EditModel : PageModel
    {
        private readonly IDentistBusiness _dentistBusiness;
        private readonly IClinicBusinessClass _clinicBusiness;

        [BindProperty]
        public Dentist Dentist { get; set; } = default!;

        public List<Clinic> Clinics { get; set; } = new List<Clinic>();

        public EditModel(IDentistBusiness dentistBusiness, IClinicBusinessClass clinicBusiness)
        {
            _dentistBusiness = dentistBusiness;
            _clinicBusiness = clinicBusiness;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "ID is null.");
                return NotFound();
            }

            var dentistResponse = await _dentistBusiness.GetById(id.ToString());
            if (dentistResponse == null)
            {
                ModelState.AddModelError(string.Empty, "No response from _dentistBusiness.GetById.");
                return NotFound();
            }

            if (dentistResponse.Data == null)
            {
                ModelState.AddModelError(string.Empty, "No data in dentistResponse.");
                return NotFound();
            }
            
            Dentist = dentistResponse.Data as Dentist;
            if (Dentist == null)
            {

                ModelState.AddModelError(string.Empty, "Data could not be cast to Dentist.");
                return NotFound();
            }

            var userId = Dentist.User?.UserId;
            if (userId == null)
            {
                ModelState.AddModelError(string.Empty, "UserId is null.");
                return NotFound();
            }

            var result = await _clinicBusiness.GetAll();
            if (result.Status != Const.SUCCESS_READ_CODE)
            {
                ModelState.AddModelError(string.Empty, "Error fetching clinics: " + result.Message);
                return Page();
            }

            var clinicResult = result.Data as List<Clinic>;
            if (clinicResult == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid clinic data received.");
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


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                
                var result = await _clinicBusiness.GetAll();
                if (result.Status != Const.SUCCESS_READ_CODE)
                {
                    ModelState.AddModelError(string.Empty, "Error fetching clinics: " + result.Message);
                    return Page();
                }

                var clinicResult = result.Data as List<Clinic>;
                if (clinicResult == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid clinic data received.");
                    return Page();
                }

                var clinics = clinicResult.Select(clinic => new SelectListItem
                {
                    Value = clinic.ClinicId.ToString(),
                    Text = clinic.ClinicName
                }).ToList();

                ViewData["ClinicId"] = new SelectList(clinics, "Value", "Text");

                
            }

            try
            {
                
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
    }


}
