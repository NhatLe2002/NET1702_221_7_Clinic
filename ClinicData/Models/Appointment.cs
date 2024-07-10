using ClinicData.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicData.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            AppoimentDetails = new HashSet<AppoimentDetail>();
        }

        public int AppointmentId { get; set; }
        [Required]

        public int CustomerId { get; set; }
        [Required]

        public string? Status { get; set; }

        [Required]
        [Range(10, 1000, ErrorMessage = "The price must be between 10 and 1000 USD.")]
        public decimal TotalPrice { get; set; }
        [Required]
        public string? PaymentMethod { get; set; }
        [Required]
        public int? PaymentStatus { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        //[Required]
        //[CustomValidation(typeof(AppointmentValidator), nameof(AppointmentValidator.Validate), ErrorMessage = "The appointment time must be in the future. ")]
        
        public DateTime? ScheduledDate { get; set; }
        [Required]
        [StringLength(50)]
        public string? Note { get; set; }

        
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; }
    }
}
