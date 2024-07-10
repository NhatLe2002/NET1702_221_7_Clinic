using System;
using System.Collections.Generic;

namespace ClinicData.Models;

public partial class Clinic
{
    public int ClinicId { get; set; }

    public string ClinicName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ClinicImage { get; set; }

    public string? ClinicDescription { get; set; }

    public DateTime? OpenTime { get; set; }

    public DateTime? CloseTime { get; set; }

    public double? IsActive { get; set; }

    public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; } = new List<AppoimentDetail>();

    public virtual ICollection<Dentist> Dentists { get; set; } = new List<Dentist>();
}
