using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicData.Models;
using ClinicBusiness;
using ClinicCommon;

namespace ClinicRazorWebApp.Pages.ClinicUpdatePage
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

        public async Task<IActionResult> OnPostAsync(IFormFile clinicImageFile)
        {
            ModelState.Remove("clinicImageFile");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (clinicImageFile != null)
            {
                var imageUrl = await _commonService.UploadAnImage(clinicImageFile, Const.PATH_IMG_CLINIC, GetUrlTail(Clinic.ClinicImage));
                Clinic.ClinicImage = imageUrl;
            }

            if (!Validation.IsValidName(Clinic.ClinicName))
            {
                ModelState.AddModelError("Clinic.ClinicName", "Tên không hợp lệ, phải bắt đầu bằng chữ viết hoa và có hơn 10 ký tự.");
                return Page();
            }

            if (!Validation.IsValidAddress(Clinic.Address))
            {
                ModelState.AddModelError("Clinic.Address", "Địa chỉ không hợp lệ, phải bao gồm cả chữ và số.");
                return Page();
            }


            if (!Validation.IsValidEmail(Clinic.Email))
            {
                ModelState.AddModelError("Clinic.Email", "Email không hợp lệ.");
                return Page();
            }

            if (Clinic.OpenTime.HasValue && Clinic.CloseTime.HasValue && !Validation.IsValidOpeningClosingTime(Clinic.OpenTime.Value, Clinic.CloseTime.Value))
            {
                ModelState.AddModelError("Clinic.OpenTime", "Giờ mở cửa phải trước giờ đóng cửa.");
                return Page();
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
