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
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // Kiểm tra tên không rỗng, bắt đầu bằng chữ viết hoa và có hơn 10 ký tự
            string pattern = @"^[A-Z][a-zA-Z\s]{9,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(name);
        }

        public static bool IsValidAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return false;

            // Kiểm tra địa chỉ bao gồm cả chữ và số
            string pattern = @"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(address);
        }

        public static bool IsValidOpeningClosingTime(TimeSpan openTime, TimeSpan closeTime)
        {
            return openTime < closeTime;
        }

    }
}
