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
using ClinicCommon;
using static System.Collections.Specialized.BitVector32;

namespace ClinicRazorWebApp.Pages.ClinicPage
{
    public class EditModel : PageModel
    {

        private readonly IClinicBusinessClass _ClinicBusiness;
        private readonly ICommonService _commonService;
        public EditModel(IClinicBusinessClass clinicBusinessClass, ICommonService commonService)
        {
            _ClinicBusiness = clinicBusinessClass;
            _commonService = commonService;
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
            if (clinic != null && clinic.Data is Clinic clinicReturn)
            {
                Clinic = clinicReturn;
                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile clinicImageFile)
        {
            ModelState.Remove("clinicImageFile");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (clinicImageFile!=null)
            {
                var imageUrl = await _commonService.UploadAnImage(clinicImageFile, Const.PATH_IMG_CLINIC, GetUrlTail(Clinic.ClinicImage));
                Clinic.ClinicImage = imageUrl;
            }
            _ClinicBusiness.Update(Clinic);

            return RedirectToPage("./Index");
        }

        private string GetUrlTail(string url)
        {
            Uri uri = new Uri(url);

            string path = uri.AbsolutePath;
            string[] segments = path.Split('/');
            string fileName = segments[^1]; 
            int dotIndex = fileName.LastIndexOf('.');
            if (dotIndex != -1)
            {
                fileName = fileName.Substring(0, dotIndex);
            }

            return fileName;
        }
    }
}
