using ClinicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData.Repository
{
    public class ClinicRepository: GenericRepository<Clinic>
    {
        protected readonly NET1702_PRN221_ClinicContext _context;
        protected readonly DbSet<Clinic> _dbSet;
        public ClinicRepository()
        {
            _context = new NET1702_PRN221_ClinicContext();
            _dbSet = _context.Set<Clinic>();
        }
        public ClinicRepository(NET1702_PRN221_ClinicContext context) => _context = context;

        
        public async Task<Clinic> GetByIdOkeAsync(int id)
        {
            return await _context.Clinics
            .Include(p => p.AppoimentDetails)
            .Include(p => p.Dentists)
            .FirstOrDefaultAsync(p => p.ClinicId == id);
        }
    }
}
