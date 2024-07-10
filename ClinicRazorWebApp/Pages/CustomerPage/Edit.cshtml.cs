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

namespace ClinicRazorWebApp.Pages.CustomerPage
{
    public class EditModel : PageModel
    {

        private readonly ICustomerBusinessClass _customercBusiness;
        private readonly ICommonService _commonService;
        private readonly IUserBusinessClass _userBusiness;

        public EditModel(ICustomerBusinessClass customerBusinessClass, ICommonService commonService, IUserBusinessClass userBusinessClass)
        {
            _customercBusiness = customerBusinessClass;
            _commonService = commonService;
            _userBusiness = userBusinessClass;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        public string MaxDate { get; set; }
        [BindProperty]
        public string UrlImage { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usersResult = await _userBusiness.GetAll();
            if (usersResult.Status == Const.SUCCESS_READ_CODE)
            {
                var listUser = usersResult.Data as List<User>;
                if (listUser != null)
                {
                    ViewData["Users"] = listUser.Select(u => new SelectListItem
                    {
                        Value = u.UserId.ToString(),
                        Text = u.Username
                    }).ToList();
                }
                else
                {
                    ViewData["Users"] = new List<SelectListItem>();
                }
            }
            MaxDate = DateTime.Today.AddYears(-18).ToString("yyyy-MM-dd");

            var customer = await _customercBusiness.GetById(id.ToString());
            if (customer != null && customer.Data is Customer customerReturn)
            {
                Customer = customerReturn;
                return Page();
            }
            else
            {
                return NotFound();
            }

        }

        public async Task<IActionResult> OnPostAsync(IFormFile customerImage)
        {
            ModelState.Remove("customerImage");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var usersResult = await _userBusiness.GetAll();
            if (usersResult.Status == Const.SUCCESS_READ_CODE)
            {
                var listUser = usersResult.Data as List<User>;
                if (listUser != null)
                {
                    ViewData["Users"] = listUser.Select(u => new SelectListItem
                    {
                        Value = u.UserId.ToString(),
                        Text = u.Username
                    }).ToList();
                }
                else
                {
                    ViewData["Users"] = new List<SelectListItem>();
                }
            }
            //Check validate
            bool isValid = true;
            //Valid Phone
            if (!Validation.IsValidPhoneNumber(Customer.Phone))
            {
                ModelState.AddModelError("ERROR", "Phone is not valid");
                isValid = false;
            }
            //Valid Email
            if (!Validation.IsValidEmail(Customer.Email))
            {
                ModelState.AddModelError("ERROR", "Email is not valid");
                isValid = false;
            }
            if (customerImage == null && String.IsNullOrEmpty(UrlImage))
            {
                ModelState.AddModelError("ERROR", "Need to input a image");
                isValid = false;
            }
            if (!isValid)
            {
                return Page();
            }

            string imageUrl;
            if (customerImage != null)
            {
                imageUrl = await _commonService.UploadAnImage(customerImage, Const.PATH_IMG_CLINIC, "Clinic" + Guid.NewGuid().ToString());
                Customer.Image = imageUrl;
            }
            else if(!String.IsNullOrEmpty(UrlImage))
            {
                Customer.Image = UrlImage;
            }

            await _customercBusiness.Update(Customer);

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
