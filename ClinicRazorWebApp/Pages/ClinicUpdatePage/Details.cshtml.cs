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
    public class DetailsModel : PageModel
    {
        private readonly IClinicBusinessClass _clinicBusinessClass;

        public DetailsModel(IClinicBusinessClass clinicBusinessClass)
        {
            _clinicBusinessClass = clinicBusinessClass;
        }

      public Clinic Clinic { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _clinicBusinessClass.GetById(id.ToString());
            if (result.Status != Const.SUCCESS_READ_CODE)
            {
                return NotFound();
            }

            Clinic = result.Data as Clinic;
            if (Clinic == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
