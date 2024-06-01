using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicData.Models;
using ClinicBusiness;

namespace ClinicRazorWebApp.Pages.ClinicPage
{
    public class CreateModel : PageModel
    {

        private readonly IClinicBusinessClass _ClinicBusiness;

        public CreateModel(IClinicBusinessClass clinicBusinessClass)
        {
            _ClinicBusiness = clinicBusinessClass;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Clinic Clinic { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid )
            {
                return Page();
            }


            var existClinic = _ClinicBusiness.GetById(Clinic.ClinicId.ToString());
            if (existClinic == null)
            {
                var clinicResult = _ClinicBusiness.Save(Clinic);
                return RedirectToPage("./Index");
            }
            else
            {
                ViewData["Message"] = "Id has exist!";
                return Page();
            }
        }
    }
}
