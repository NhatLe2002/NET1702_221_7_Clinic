using ClinicBusiness;
using ClinicCommon;
using ClinicData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Net;
using System.Xml.Linq;

namespace ClinicRazorWebApp.Pages.AppoinmentPage
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentBusinessClass _AppointmentBusiness;
        



        public IndexModel(IAppointmentBusinessClass appointmentBusiness)
        {
            _AppointmentBusiness = appointmentBusiness;
            
          
        }

        public IList<Appointment> Appointment { get; set; } = default!;
        public IList<Customer> Customer { get; set; } = default!;


        public int PageSize { get; set; } = 3;
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalAppointment { get; set; } = 20;

        [BindProperty(SupportsGet = true)]
        public string totalPrice { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? status { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? paymentMethod { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? scheduledDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? note { get; set; }
        public async Task OnGetAsync(int? pageIndex)
        {
            //Appointment = this.GetAppointments();
            //customer = this.getcustomers();
            PageIndex = pageIndex ?? 1;
            List<Appointment> pagedAppointments;
            List<Appointment> appointmentResult = (List<Appointment>)((await _AppointmentBusiness.GetAll())?.Data);
            if (!string.IsNullOrEmpty(totalPrice))
            {
                appointmentResult = appointmentResult.Where(a => a.TotalPrice.ToString().Contains(totalPrice)).ToList();
            }
            if (!string.IsNullOrEmpty(status))
            {
                appointmentResult = appointmentResult.Where(a => a.Status.Contains(status)).ToList();
            }
            if (!string.IsNullOrEmpty(paymentMethod))
            {
                appointmentResult = appointmentResult.Where(a => a.PaymentMethod.Contains(paymentMethod)).ToList();
            }
            if (!string.IsNullOrEmpty(note))
            {
                appointmentResult = appointmentResult.Where(a => a.Note.Contains(note)).ToList();
            }
            if (!string.IsNullOrEmpty(scheduledDate))
            {
                appointmentResult = appointmentResult.Where(a => a.ScheduledDate.ToString().Contains(scheduledDate)).ToList();
            }
            pagedAppointments = PaginationHelper.GetPaged(appointmentResult, PageIndex, PageSize);
            //Handle when delete customer and page no any customer
            if ((pagedAppointments == null || pagedAppointments.Count == 0) && pageIndex - 1 > 0)
            {
                PageIndex--;
                pagedAppointments = PaginationHelper.GetPaged(appointmentResult, PageIndex, PageSize);
            }

            TotalAppointment = appointmentResult.Count;
            if             (TotalAppointment == null)
            {
                System.Console.WriteLine("tôi null");
            }


            TotalPages = (int)Math.Ceiling(appointmentResult.Count / (double)PageSize);
            Appointment = pagedAppointments;


        }

    
        private List<Appointment> GetAppointments()
        {
            var appointmentResult = _AppointmentBusiness.GetAll();



            if (appointmentResult.Status > 0 && appointmentResult.Result.Data != null)
            {
                var appointment = (List<Appointment>)appointmentResult.Result.Data;
                return appointment;
            }
            return new List<Appointment>();
        }
        
        

        
        private List<Customer> GetCustomers()
        {
            var custometResult = _AppointmentBusiness.GetAllCustomer();

            if (custometResult.Status > 0 && custometResult.Result.Data != null)
            {
                var customer = (List<Customer>)custometResult.Result.Data;
                return customer;
            }
            return new List<Customer>();
        }
    }
}

