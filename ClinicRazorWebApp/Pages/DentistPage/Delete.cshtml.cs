using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;
using DentistBusiness;
using ClinicCommon;

namespace ClinicRazorWebApp.Pages.DentistPage
{
    public class DeleteModel : PageModel
    {
        
        private readonly IDentistBusiness _dentistBusiness;

        public DeleteModel(IDentistBusiness dentistBusiness)
        {
            _dentistBusiness = dentistBusiness;
        }

        [BindProperty]
      public Dentist Dentist { get; set; } = default!;

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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "ID is null.");
                return NotFound();
            }

            var dentistResponse = await _dentistBusiness.GetById(id.ToString());
            if (dentistResponse == null || dentistResponse.Data == null)
            {
                ModelState.AddModelError(string.Empty, "No data found for the given ID.");
                return NotFound();
            }

            Dentist = dentistResponse.Data as Dentist;
            if (Dentist == null)
            {
                ModelState.AddModelError(string.Empty, "Data could not be cast to Dentist.");
                return NotFound();
            }

            var deleteResponse = await _dentistBusiness.DeleteById(id.ToString());
            if (deleteResponse == null || deleteResponse.Status != Const.SUCCESS_DELETE_CODE)
            {
                var errorMessage = deleteResponse?.Message ?? "Error deleting dentist.";
                ModelState.AddModelError(string.Empty, errorMessage);
                return Page();
            }

            return RedirectToPage("./Index"); // Redirect to a relevant page after deletion
        }



    }
}
