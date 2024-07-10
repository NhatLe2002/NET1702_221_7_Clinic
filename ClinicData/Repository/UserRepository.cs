using ClinicData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        protected readonly NET1702_PRN221_ClinicContext _context;
        protected readonly DbSet<User> _dbSet;
        public UserRepository()
        {
            _context = new NET1702_PRN221_ClinicContext();
            _dbSet = _context.Set<User>();
        }
        public UserRepository(NET1702_PRN221_ClinicContext context) => _context = context;
    }
}
