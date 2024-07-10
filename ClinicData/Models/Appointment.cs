using System;
using System.Collections.Generic;

namespace ClinicData.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int CustomerId { get; set; }

    public string? Status { get; set; }

    public decimal TotalPrice { get; set; }

    public string? PaymentMethod { get; set; }

    public int? PaymentStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ScheduledDate { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; } = new List<AppoimentDetail>();

    public virtual Customer Customer { get; set; } = null!;
}
