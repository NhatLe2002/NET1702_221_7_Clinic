using ClinicData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData.Validation
{
    public class DentistValidation 
    {
        public class MinAgeAttribute : ValidationAttribute
        {
            private readonly int _minAge;

            public MinAgeAttribute(int minAge)
            {
                _minAge = minAge;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateTime dateOfBirth)
                {
                    var today = DateTime.Today;
                    var age = today.Year - dateOfBirth.Year;
                    if (dateOfBirth.Date > today.AddYears(-age)) age--;

                    if (age < _minAge)
                    {
                        return new ValidationResult(ErrorMessage ?? $"Chưa đủ tuổi hành nghề (ít nhất {_minAge} tuổi).");
                    }
                }

                return ValidationResult.Success;
            }
        }

        public static ValidationResult ValidateUniqueEmail(string email, ValidationContext validationContext)
        {
            var dentist = (Dentist)validationContext.ObjectInstance;

            using (var _context = new NET1702_PRN221_ClinicContext())
            {
                // Kiểm tra xem có bác sĩ nào khác đã sử dụng email này chưa
                var existingDentist = _context.Dentists.FirstOrDefault(d => d.DentistId != dentist.DentistId && d.Email == email);

                if (existingDentist == null)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Email đã tồn tại.");
            }
        }

        public static ValidationResult ValidateUniquePhone(string phone, ValidationContext validationContext)
        {
            var dentist = (Dentist)validationContext.ObjectInstance;

            using (var _context = new NET1702_PRN221_ClinicContext())
            {
                // Kiểm tra xem có bác sĩ nào khác đã sử dụng số điện thoại này chưa
                var existingDentist = _context.Dentists.FirstOrDefault(d => d.DentistId != dentist.DentistId && d.Phone == phone);

                if (existingDentist == null)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Số điện thoại đã tồn tại.");
            }
        }
    }
}

