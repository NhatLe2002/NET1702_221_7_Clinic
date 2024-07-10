using ClinicBusiness;
using ClinicCommon;
using ClinicData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicRazorWebApp.Pages.AppoinmentPage
{
    public class CreateModel : PageModel
    {
        private readonly IAppointmentBusinessClass _AppointmentBusiness;
        private readonly ICommonService _commonService;

        public CreateModel(IAppointmentBusinessClass appointmentBusiness, ICommonService commonService)
        {
            _AppointmentBusiness = appointmentBusiness;
            _commonService = commonService;
        }
        [BindProperty]
        public Appointment appointment { get; set; } = default!;

        public List<Customer> customer { get; set; } = new List<Customer>();


        public async Task<IActionResult> OnGet()
        {
            var result = await _AppointmentBusiness.GetAllCustomer();
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

            ViewData["Customer"] = new SelectList(customers,  "Value", "Text");

            return Page();
        }




        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var result = await _AppointmentBusiness.GetAllCustomer();
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
                }).ToList(), "Value", "Text");  ;

            }
            var appoinmentResult = _AppointmentBusiness.Save(appointment);
            return RedirectToPage("./Index");

        }




        
        
    }
}
