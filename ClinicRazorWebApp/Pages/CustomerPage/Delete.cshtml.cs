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
    public class DeleteModel : PageModel
    {
        private readonly ICustomerBusinessClass _customerBusiness;

        public DeleteModel(ICustomerBusinessClass customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }
        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)

            {
                return NotFound();
            }

            var customer = await _customerBusiness.GetById(id.ToString());

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                if (customer.Data is Customer customerResult)
                {

                    Customer = customerResult;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _customerBusiness.DeleteById(id.ToString());
            /*var clinic = await _ClinicBusiness.GetById(id.ToString());

            if (clinic != null)
            {
                _ClinicBusiness.DeleteById(id.ToString());
            }*/

            return RedirectToPage("./Index");
        }
    }
}
