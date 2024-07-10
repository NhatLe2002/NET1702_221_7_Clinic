using System;
using System.Collections.Generic;

namespace ClinicData.Models;

public partial class ExaminationResult
{
    public int ExaminationResultId { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public bool? IsActive { get; set; }

    public DateOnly? ReExaminationDate { get; set; }

    public string? Diagnosis { get; set; }

    public string? Prescription { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; } = new List<AppoimentDetail>();
}
