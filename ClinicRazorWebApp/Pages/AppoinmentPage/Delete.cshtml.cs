using ClinicBusiness;
using ClinicData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicRazorWebApp.Pages.AppoinmentPage
{
    public class DeleteModel : PageModel
    {
        private readonly IAppointmentBusinessClass _appoinmentBusiness;

        public DeleteModel(IAppointmentBusinessClass appointment)
        {
            _appoinmentBusiness = appointment;
        }
        [BindProperty]
        public Appointment Appointment { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)

            {
                return NotFound();
            }

            var appointment = await _appoinmentBusiness.GetById(id.ToString());

            if (appointment == null)
            {
                return NotFound();
            }
            else
            {
                if (appointment.Data is Appointment appoinmentResult)
                {

                    Appointment = appoinmentResult;
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
            _appoinmentBusiness.DeleteById(id.ToString());
            /*var clinic = await _ClinicBusiness.GetById(id.ToString());

            if (clinic != null)
            {
                _ClinicBusiness.DeleteById(id.ToString());
            }*/

            return RedirectToPage("./Index");
        }
    }
}
