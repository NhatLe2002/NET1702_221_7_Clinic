using ClinicBusiness;
using ClinicCommon;
using ClinicData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicRazorWebApp.Pages.AppoinmentPage
{
    public class EditModel : PageModel
    {

        private readonly IAppointmentBusinessClass _appoinmentBusiness;

        public EditModel(IAppointmentBusinessClass appointmentBusiness)
        {
            _appoinmentBusiness = appointmentBusiness;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _appoinmentBusiness.GetAllCustomer();
            if (result.Status != Const.SUCCESS_READ_CODE)
            {
                return Page();
            }

            var customerResult = result.Data as List<Customer>;
            if (customerResult == null)
            {
                return Page();
            }

            var customers = customerResult.Select(customer => new SelectListItem
            {
                Value = customer.CustomerId.ToString(),
                Text = customer.CustomerName
            }).ToList();

            ViewData["Customer"] = new SelectList(customers, "Value", "Text");

            var appointment = await _appoinmentBusiness.GetById(id.ToString());
            if (appointment != null && appointment.Data is Appointment appointmentReturn)
            {
                Appointment = appointmentReturn;
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
                var result = await _appoinmentBusiness.GetAllCustomer();
                if (result == null || result.Status != Const.SUCCESS_READ_CODE)
                {
                    return Page();
                }

                var customerResult = result.Data as List<Customer>;
                if (customerResult == null)
                {
                    return Page();
                }

                ViewData["Customer"] = new SelectList(customerResult.Select(customer => new SelectListItem
                {
                    Value = customer.CustomerId.ToString(),
                    Text = customer.CustomerName
                }).ToList(), "Value", "Text"); ;

            }

            _appoinmentBusiness.Update(Appointment);

            return RedirectToPage("./Index");
        }
    }
}
