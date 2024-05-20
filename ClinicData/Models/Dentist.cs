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
        public int? UserId { get; set; }
        public int? ClinicId { get; set; }
        public string? DentistName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? ProfessionalQualifications { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public byte[]? Image { get; set; }

        public virtual Clinic? Clinic { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; }
    }
}
