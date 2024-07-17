using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ClinicCommon.Validation;

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

        [Required(ErrorMessage = "Dentist name is required")]
        [StringLength(100, ErrorMessage = "Dentist name cannot be longer than 100 characters")]
        public string DentistName { get; set; } = null!;

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        [MinimumAge(24, ErrorMessage = "Dentist must be at least 24 years old.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = null!;

        [Required(ErrorMessage = "Professional qualifications are required")]
        public string ProfessionalQualifications { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
        public string? Image { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = null!;

        public virtual Clinic Clinic { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; }
    }
}