using System;
using System.Collections.Generic;

namespace ClinicData.Models
{
    public partial class Dentist
    {
        public Dentist()
        {
            AppoimentDetails = new HashSet<AppoimentDetail>();
        }

        public int DentistId { get; set; }
        public int UserId { get; set; }
        public int ClinicId { get; set; }
        public string DentistName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string ProfessionalQualifications { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Image { get; set; }
        public string Address { get; set; } = null!;

        public virtual Clinic Clinic { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; }
    }
}
