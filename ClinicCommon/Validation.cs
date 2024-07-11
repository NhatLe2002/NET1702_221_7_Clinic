using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClinicCommon
{
    public class Validation
    {
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;
            // Biểu thức chính quy để kiểm tra số điện thoại
            string pattern = @"^\+?[0-9]{10,15}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Biểu thức chính quy để kiểm tra email hợp lệ
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                Regex regex = new Regex(pattern);

                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

    }
}
