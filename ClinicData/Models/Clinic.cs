using System;
using System.Collections.Generic;

namespace ClinicData.Models
{
    public partial class Clinic
    {
        public Clinic()
        {
            AppoimentDetails = new HashSet<AppoimentDetail>();
            Dentists = new HashSet<Dentist>();
        }

        public int ClinicId { get; set; }
        public string? ClinicName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public byte[]? ClinicImage { get; set; }
        public string? ClinicDescription { get; set; }

        public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; }
        public virtual ICollection<Dentist> Dentists { get; set; }
    }
}
