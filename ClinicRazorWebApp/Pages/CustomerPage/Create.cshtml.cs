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
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace ClinicRazorWebApp.Pages.CustomerPage
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerBusinessClass _customerBusiness;
        private readonly ICommonService _commonService;
        private readonly IUserBusinessClass _userBusiness;

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        public string MaxDate { get; set; }

        public CreateModel(ICustomerBusinessClass customerBusinessClass, ICommonService commonService, IUserBusinessClass userBusinessClass)
        {
            _customerBusiness = customerBusinessClass;
            _commonService = commonService;
            _userBusiness = userBusinessClass;
        }

        public async Task<IActionResult> OnGet()
        {
            var usersResult = await _userBusiness.GetAll();
            if (usersResult.Status == Const.SUCCESS_READ_CODE)
            {
                var listUser = usersResult.Data as List<User>;
                if (listUser!=null)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile customerImageFile)
        {
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
            if (!isValid)
            {
                return Page();
            }
            //Save img to cloudinary
            var imageUrl = await _commonService.UploadAnImage(customerImageFile, Const.PATH_IMG_CUSTOMER, "Clinic" + Guid.NewGuid().ToString());
            Customer.Image = imageUrl;

            var customerResult = _customerBusiness.Save(Customer);
            return RedirectToPage("./Index");
        }
    }
}
