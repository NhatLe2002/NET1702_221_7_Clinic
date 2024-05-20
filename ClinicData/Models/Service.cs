using System;
using System.Collections.Generic;

namespace ClinicData.Models
{
    public partial class Service
    {
        public Service()
        {
            AppoimentDetails = new HashSet<AppoimentDetail>();
        }

        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<AppoimentDetail> AppoimentDetails { get; set; }
    }
}
