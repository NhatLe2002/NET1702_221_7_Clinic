using ClinicBusiness;
using ClinicData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicRazorWebApp.Pages.AppoinmentPage
{
    public class DetailsModel : PageModel
    {
        private readonly IAppointmentBusinessClass _appoinmentBusiness;

        public DetailsModel(IAppointmentBusinessClass appointmentBusiness)
        {
            _appoinmentBusiness = appointmentBusiness;
        }

        public Appointment Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appoinmentBusiness.GetById(id.ToString());
            if (appointment.Status != -1)
            {
                Appointment = (Appointment)appointment.Data;
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _appoinmentBusiness.DeleteById(id.ToString());
            TempData["Message"] = "Delete successfully";
            return RedirectToPage("./Index");
        }
    }
}
