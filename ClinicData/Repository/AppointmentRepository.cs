using ClinicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

    }
}