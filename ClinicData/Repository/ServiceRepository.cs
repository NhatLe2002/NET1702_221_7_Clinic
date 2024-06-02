using ClinicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData.Repository
{
    public class ServiceRepository : GenericRepository<Service>
    {
        protected readonly NET1702_PRN221_ClinicContext _context;
        protected readonly DbSet<Service> _dbSet;
        public ServiceRepository()
        {
            _context = new NET1702_PRN221_ClinicContext();
            _dbSet = _context.Set<Service>();
        }
        public ServiceRepository(NET1702_PRN221_ClinicContext context) => _context ??= context;

        public async Task<Service> GetByIdOkeAsync(int id)
        {
            return await _context.Services
            .Include(p => p.AppoimentDetails)
            .FirstOrDefaultAsync(p => p.ServiceId == id);
        }
    }
}
