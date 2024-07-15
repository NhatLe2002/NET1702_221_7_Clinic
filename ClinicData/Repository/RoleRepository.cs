using ClinicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData.Repository
{
    public class RoleRepository : GenericRepository<Role>
    {
        protected readonly NET1702_PRN221_ClinicContext _context;
        protected readonly DbSet<Role> _dbSet;
        public RoleRepository()
        {
            _context = new NET1702_PRN221_ClinicContext();
            _dbSet = _context.Set<Role>();
        }
        public RoleRepository(NET1702_PRN221_ClinicContext context) => _context ??= context;

        public async Task<Role> GetByIdOkeAsync(int id)
        {
            return await _context.Roles
            .Include(p => p.Users)
            .FirstOrDefaultAsync(p => p.RoleId == id);
        }
    }
}
