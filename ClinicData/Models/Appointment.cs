using System;
using System.Collections.Generic;

namespace ClinicData.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            AppoimentDetails = new HashSet<AppoimentDetail>();
        }

        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public string? Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string? PaymentMethod { get; set; }
        public int? PaymentStatus { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; }
    }
}
