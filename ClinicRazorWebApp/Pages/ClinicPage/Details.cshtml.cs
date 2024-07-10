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
    public class DetailsModel : PageModel
    {
        private readonly IClinicBusinessClass _ClinicBusiness;

        public DetailsModel(IClinicBusinessClass clinicBusinessClass)
        {
            _ClinicBusiness = clinicBusinessClass;
        }

      public Clinic Clinic { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _ClinicBusiness.GetById(id.ToString());
            if(clinic.Status!=-1)
            {
                Clinic = (Clinic)clinic.Data;
            }
            
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null )
            {
                return NotFound();
            }
            await _ClinicBusiness.DeleteById(id.ToString());
            TempData["Message"] = "Delete successfully";
            return RedirectToPage("./Index");
        }
    }
}
