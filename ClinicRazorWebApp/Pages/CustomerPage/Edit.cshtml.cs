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

namespace ClinicRazorWebApp.Pages.CustomerPage
{
    public class EditModel : PageModel
    {

        private readonly ICustomerBusinessClass _customercBusiness;

        public EditModel(ICustomerBusinessClass customerBusinessClass)
        {
            _customercBusiness = customerBusinessClass;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _customercBusiness.GetById(id.ToString());
            if (clinic != null && clinic.Data is Customer customerReturn)
            {
                Customer = customerReturn;
                return Page();
            }
            else
            {
                return NotFound();
            }

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _customercBusiness.Update(Customer);

            return RedirectToPage("./Index");
        }
    }
}
