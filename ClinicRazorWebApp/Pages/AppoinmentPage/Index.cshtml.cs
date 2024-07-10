using ClinicBusiness;
using ClinicCommon;
using ClinicData.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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
       
        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalAppointment { get; set; } = 20;
        public ActionResult excelFile { get; set; }

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
            List<Appointment> appointmentResult = (List<Appointment>)((await _AppointmentBusiness.Search(SearchTerm))?.Data);
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

            TotalAppointment = appointmentResult?.Count ?? 0;
            TotalPages = PaginationHelper.GetTotalPages(appointmentResult, PageSize);
            Appointment = pagedAppointments;

           

        }
        public async Task<IActionResult> OnPostExportExcelAsync()
        {

            var myBUs = await _AppointmentBusiness.GetAll();
            // above code loads the data using LINQ with EF (query of table), you can substitute this with any data source.
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                                package.Save();
            }
            stream.Position = 0;

            string excelName = $"BusinessUnits-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            // above I define the name of the file using the current datetime.

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName); // this will be the actual export.
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
        
        private ActionResult ExportExcel()
        {
            var _empdata = _AppointmentBusiness.GetEmpData();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(_empdata, "AppointmentRecord");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Appointment.xlsx");
                }
            }
        }
    }
}

