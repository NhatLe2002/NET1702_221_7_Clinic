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
        private readonly IClinicBusinessClass _clinicBusiness;
        private readonly ICommonService _commonService;

        public CreateModel(IClinicBusinessClass clinicBusinessClass, ICommonService commonService)
        {
            _clinicBusiness = clinicBusinessClass;
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

            if (!Validation.IsValidPhoneNumber(Clinic.Phone))
            {
                ModelState.AddModelError("Clinic.Phone", "Số điện thoại không hợp lệ.");
                return Page();
            }

            if (Clinic.OpenTime.HasValue && Clinic.CloseTime.HasValue && !Validation.IsValidOpeningClosingTime(Clinic.OpenTime.Value, Clinic.CloseTime.Value))
            {
                ModelState.AddModelError("Clinic.OpenTime", "Giờ mở cửa phải trước giờ đóng cửa.");
                return Page();
            }

            var imageUrl = await _commonService.UploadAnImage(clinicImageFile, Const.PATH_IMG_CLINIC, "Clinic" + Guid.NewGuid().ToString());
            Clinic.ClinicImage = imageUrl;

            var clinicResult = await _clinicBusiness.Save(Clinic);
            if (clinicResult.Status != Const.SUCCESS_CREATE_CODE)
            {
                ModelState.AddModelError(string.Empty, "Error creating Clinic: " + clinicResult.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
