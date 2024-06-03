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
    public class IndexModel : PageModel
    {
        private readonly ICustomerBusinessClass _customerBusiness;

        public IndexModel(ICustomerBusinessClass customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        public IList<Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Customer = this.GetAllCustomer();
            /* if (_context.Clinics != null)
             {
                 Clinic = await _context.Clinics.ToListAsync();
             }*/
        }
        private List<Customer> GetAllCustomer()
        {
            var customerResult = _customerBusiness.GetAll();

            if (customerResult.Status > 0 && customerResult.Result.Data != null)
            {
                var customers = (List<Customer>)customerResult.Result.Data;
                return customers;
            }
            return new List<Customer>();
        }
    }
}
