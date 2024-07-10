using System;
using System.Collections.Generic;

namespace ClinicData.Models;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }

    public string Phone { get; set; }

    public string? Address { get; set; }

    public DateTime Birthday { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Dentist> Dentists { get; set; } = new List<Dentist>();

    public virtual Role Role { get; set; } = null!;
}
