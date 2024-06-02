using ClinicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        protected readonly NET1702_PRN221_ClinicContext _context;
        protected readonly DbSet<Clinic> _dbSet;
        public CustomerRepository()
        {
            _context = new NET1702_PRN221_ClinicContext();
            _dbSet = _context.Set<Clinic>();
        }
        public CustomerRepository(NET1702_PRN221_ClinicContext context) => _context = context;
    }
}
