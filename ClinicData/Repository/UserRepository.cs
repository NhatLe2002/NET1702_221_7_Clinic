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
        public UserRepository(NET1702_PRN221_ClinicContext context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }
        public async Task<User> GetByIdOkeAsync(int id)
        {
            return await _context.Users
            .Include(p => p.Customers)
            .Include(p => p.Dentists)
            .FirstOrDefaultAsync(p => p.UserId == id);
        }
        public async Task<List<User>> GetAllUserAsync()
        {
           /* return await _dbSet
                .Include(u => u.Role.RoleName) // Assuming User entity has a navigation property to Role
                .ToListAsync();*/
           return await _context.Users.Include(p => p.Role).ToListAsync();
        }
        public async Task<User> GetByIdUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("The userId cannot be null or empty.", nameof(userId));
            }

            if (!int.TryParse(userId, out int id))
            {
                throw new ArgumentException("The userId must be a valid integer.", nameof(userId));
            }

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return user;
        }

    }
}
