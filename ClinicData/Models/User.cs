using System;
using System.Collections.Generic;

namespace ClinicData.Models
{
    public partial class User
    {
        public User()
        {
            Customers = new HashSet<Customer>();
            Dentists = new HashSet<Dentist>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Dentist> Dentists { get; set; }
    }
}
