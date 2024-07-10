using System;
using System.Collections.Generic;

namespace ClinicData.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public TimeOnly? Duration { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Category { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; } = new List<AppoimentDetail>();
}
