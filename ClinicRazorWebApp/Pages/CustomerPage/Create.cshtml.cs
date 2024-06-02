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

namespace ClinicRazorWebApp.Pages.CustomerPage
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerBusinessClass _customerBusiness;
        private readonly ICommonService _commonService;

        public CreateModel(ICustomerBusinessClass clinicBusinessClass, ICommonService commonService)
        {
            _customerBusiness = clinicBusinessClass;
            _commonService = commonService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile customerImageFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var imageUrl = await _commonService.UploadAnImage(customerImageFile, Const.PATH_IMG_CUSTOMER, "Clinic" + Guid.NewGuid().ToString());
            Customer.Image = imageUrl;
            var customerResult = _customerBusiness.Save(Customer);
            return RedirectToPage("./Index");
        }
    }
}
