using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;

namespace ClinicRazorWebApp.Pages.CustomerPage
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerBusinessClass _customerBusiness;

        public DetailsModel(ICustomerBusinessClass customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerBusiness.GetById(id.ToString());
            if (customer.Status != -1)
            {
                Customer = (Customer)customer.Data;
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _customerBusiness.DeleteById(id.ToString());
            TempData["Message"] = "Delete successfully";
            return RedirectToPage("./Index");
        }
    }
}
