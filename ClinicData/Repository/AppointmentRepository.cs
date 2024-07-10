using ClinicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>
    {
        protected readonly NET1702_PRN221_ClinicContext _context;
        protected readonly DbSet<Appointment> _dbSet;
        protected readonly DbSet<Customer> _customerDb;
        private List<Appointment> _appoinment;
        public AppointmentRepository()
        {
            _context = new NET1702_PRN221_ClinicContext();
            _dbSet = _context.Set<Appointment>();
            _customerDb = _context.Set<Customer>();

        }
        public AppointmentRepository(NET1702_PRN221_ClinicContext context) => _context = context;


        public async Task<List<Customer>> GetAllCustomer()
        {
            return await _customerDb.ToListAsync();
        }

        public async Task<Appointment> GetByIdOkeAsync(int id)
        {
            return await _context.Appointments
            .Include(p => p.Customer)
            .FirstOrDefaultAsync(p => p.AppointmentId == id);
        }

        public async Task<Customer> GetCustomerById(string code)
        {
            return await _customerDb.FindAsync(int.Parse(code));
        }

        public async Task<List<Appointment>> SearchAppointmentsAsync(string SearchTerm)
        {
            if(string.IsNullOrWhiteSpace(SearchTerm))
            {
                return await _context.Appointments.ToListAsync();
            }
            return await _context.Appointments
                .Where(p => p.Status.Contains(SearchTerm) ||
                            p.PaymentMethod.Contains(SearchTerm) ||
                            p.TotalPrice.ToString().Contains(SearchTerm)
                ) 
                .ToListAsync();
        }
        public DataTable GetEmpData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Appointment data";
            dt.Columns.Add("AppointmentID", typeof(int));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Total Price", typeof(Decimal));
            dt.Columns.Add("Payment Method", typeof(string));
            dt.Columns.Add("ScheduledDate", typeof(DateTime));
            dt.Columns.Add("Note", typeof(string));

            var _list = _context.Appointments.ToList();
            if (_list.Count > 0) 
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.AppointmentId, item.Status, item.TotalPrice, item.PaymentMethod, item.ScheduledDate, item.Note);
                });
                
            }
            return dt;
        }
         
    }
}