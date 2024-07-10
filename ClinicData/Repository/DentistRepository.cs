using ClinicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData.Repository
{
    public class DentistRepository : GenericRepository<Dentist>
    {
        protected readonly NET1702_PRN221_ClinicContext _context;
        protected readonly DbSet<Dentist> _dbSet;
        public DentistRepository()
        {
            _context = new NET1702_PRN221_ClinicContext();
            _dbSet = _context.Set<Dentist>();
        }
        public DentistRepository(NET1702_PRN221_ClinicContext context) => _context = context;
        public async Task<List<Dentist>> GetAllWithDetailsAsync()
        {
            return await _context.Dentists.Include(c => c.Clinic).ToListAsync();
        }

        public async Task<Dentist> GetByIdWithDetailsAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return null;
            }
            return await _context.Dentists
                                            .Include(c => c.Clinic)
                                            .Include(u => u.User)
                                            .FirstOrDefaultAsync(d => d.DentistId.ToString() == Id);
        }

        
    }
}
