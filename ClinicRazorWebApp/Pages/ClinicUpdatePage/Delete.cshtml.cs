using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;
using ClinicCommon;

namespace ClinicRazorWebApp.Pages.ClinicUpdatePage
{
    public class DeleteModel : PageModel
    {
        private readonly IClinicBusinessClass _ClinicBusiness;

        public DeleteModel(IClinicBusinessClass clinicBusinessClass)
        {
            _ClinicBusiness = clinicBusinessClass;
        }
        [BindProperty]
        public Clinic Clinic { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)

            {
                return NotFound();
            }

            var clinic = await _ClinicBusiness.GetById(id.ToString());

            if (clinic == null)
            {
                return NotFound();
            }
            else
            {
                if (clinic.Data is Clinic clinicResult)
                {

                    Clinic = clinicResult;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _ClinicBusiness.DeactivateClinic(id.ToString());

            if (result.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                // Handle error
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }
        }
    }
}
