using ClinicData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData.Validator
{
    public class AppointmentValidator : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is Appointment appointment &&
                appointment.ScheduledDate < DateTime.Now)
            {
                yield return new ValidationResult("The time for scheduling must be in the future..", new[] { nameof(Appointment.ScheduledDate) });
            }
            
        }
    }
}
