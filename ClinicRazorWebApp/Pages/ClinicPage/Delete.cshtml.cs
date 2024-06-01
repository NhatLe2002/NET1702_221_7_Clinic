using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;

namespace ClinicRazorWebApp.Pages.ClinicPage
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
            if (id == null )
            {
                return NotFound();
            }
            _ClinicBusiness.DeleteById(id.ToString());
            /*var clinic = await _ClinicBusiness.GetById(id.ToString());

            if (clinic != null)
            {
                _ClinicBusiness.DeleteById(id.ToString());
            }*/

            return RedirectToPage("./Index");
        }
    }
}
