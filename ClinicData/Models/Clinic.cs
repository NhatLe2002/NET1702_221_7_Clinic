using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "ClinicName is required")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "ClinicName must be between 10 and 100 characters")]
        [RegularExpression(@"^[A-ZÀ-Ỹ][a-zA-ZÀ-ỹà-ỹ0-9\s]*$", ErrorMessage = "ClinicName must start with a capital letter, can contain Vietnamese characters, numbers, and spaces, but no special characters")]
        public string ClinicName { get; set; } = null!;
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})\b", ErrorMessage = "Phone number is not valid. It should be a valid Vietnamese phone number.")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        public string Email { get; set; } = null!;
        public string? ClinicImage { get; set; }
        public string? ClinicDescription { get; set; }

        public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; }
        public virtual ICollection<Dentist> Dentists { get; set; }
    }
}
