using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicBusiness;
using ClinicData.Models;


namespace ClinicRazorWebApp.Pages.DentistPage
{
    public class DetailsModel : PageModel
    {
        private readonly IDentistBusiness _dentistBusiness;

        public DetailsModel(IDentistBusiness dentistBusiness)
        {
            _dentistBusiness = dentistBusiness;
        }

        public Dentist Dentist { get; set; } = default!;

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

            return Page();
        }
    }
}
