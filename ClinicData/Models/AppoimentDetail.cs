using System;
using System.Collections.Generic;

namespace ClinicData.Models
{
    public partial class AppoimentDetail
    {
        public int AppoimnetDetailId { get; set; }
        public int ClinicId { get; set; }
        public int DentistId { get; set; }
        public int ServiceId { get; set; }
        public int AppointmentId { get; set; }
        public int? ExaminationResultId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Date { get; set; }

        public virtual Appointment Appointment { get; set; } = null!;
        public virtual Clinic Clinic { get; set; } = null!;
        public virtual Dentist Dentist { get; set; } = null!;
        public virtual ExaminationResult? ExaminationResult { get; set; }
        public virtual Service Service { get; set; } = null!;
    }
}
