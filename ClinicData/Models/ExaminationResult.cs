using System;
using System.Collections.Generic;

namespace ClinicData.Models
{
    public partial class ExaminationResult
    {
        public ExaminationResult()
        {
            AppoimentDetails = new HashSet<AppoimentDetail>();
        }

        public int ExaminationResultId { get; set; }
        public string? Content { get; set; }
        public DateTime? Date { get; set; }
        public byte[]? ExaminationFile { get; set; }
        public DateTime? ReExaminationDate { get; set; }

        public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; }
    }
}
