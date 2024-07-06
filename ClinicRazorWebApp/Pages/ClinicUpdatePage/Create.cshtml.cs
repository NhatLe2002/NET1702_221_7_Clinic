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

namespace ClinicRazorWebApp.Pages.ClinicUpdatePage
{
    public class CreateModel : PageModel
    {
        private readonly IClinicBusinessClass _ClinicBusiness;
        private readonly ICommonService _commonService;

        public CreateModel(IClinicBusinessClass clinicBusinessClass, ICommonService commonService)
        {
            _ClinicBusiness = clinicBusinessClass;
            _commonService = commonService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Clinic Clinic { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile clinicImageFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var imageUrl = await _commonService.UploadAnImage(clinicImageFile, Const.PATH_IMG_CLINIC, "Clinic" + Guid.NewGuid().ToString());
            Clinic.ClinicImage = imageUrl;
            var clinicResult = _ClinicBusiness.Save(Clinic);
            return RedirectToPage("./Index");
        }
    }
}
