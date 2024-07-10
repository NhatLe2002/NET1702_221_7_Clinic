using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ClinicData.Validation.DentistValidation;

namespace ClinicData.Models;

public partial class Dentist
{
    [Required]
    public int DentistId { get; set; }

    public int UserId { get; set; }

    [Required]
    public int ClinicId { get; set; }

    [Required]
    [StringLength(100)]
    public string DentistName { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    [MinAge(24, ErrorMessage = "Chưa đủ tuổi hành nghề (ít nhất 24 tuổi).")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string Gender { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string ProfessionalQualifications { get; set; } = null!;

    [Required]
    [Phone]
    public string Phone { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Url]
    public string? Image { get; set; }
    [Required]
    [StringLength(200)]
    public string? Address { get; set; }

    public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; } = new List<AppoimentDetail>();

    public virtual Clinic Clinic { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
