using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;
using ClinicCommon;
using System.Text.RegularExpressions;

namespace ClinicRazorWebApp.Pages.UserPage
{
    public class EditModel : PageModel
    {
        private readonly IUserBusiness _UserBusiness;

        public EditModel(IUserBusiness userBusiness)
        {
            _UserBusiness = userBusiness;
        }
        //private readonly ClinicData.Models.NET1702_PRN221_ClinicContext _context;

        /*public EditModel(ClinicData.Models.NET1702_PRN221_ClinicContext context)
        {
            _context = context;
        }*/

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _UserBusiness.GetByIdUser(id.ToString());

            if (user != null && user.Data is User userReturn)
            {
                User = userReturn;

                var roles = await _UserBusiness.GetRoles();
                if (roles != null)
                {
                    ViewData["Roles"] = roles.Select(u => new SelectListItem
                    {
                        Value = u.RoleId.ToString(),
                        Text = u.RoleName
                    }).ToList();
                }
                else
                {
                    ViewData["Roles"] = new List<SelectListItem>();
                }

                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 12)
                return false;

            // Check for at least one uppercase letter
            if (!password.Any(char.IsUpper))
                return false;

            // Check for at least one lowercase letter
            if (!password.Any(char.IsLower))
                return false;

            // Check for at least one digit
            if (!password.Any(char.IsDigit))
                return false;

            // Check for at least one special character
            if (!Regex.IsMatch(password, @"[\W_]"))
                return false;

            // Check for whitespace characters
            if (password.Any(char.IsWhiteSpace))
                return false;

            return true;
        }

        private bool ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username) || username.Length < 7 || username.Contains(' '))
                return false;
            return true;
        }

        private bool ValidateBirthday(DateTime? birthday)
        {
            if (birthday == null)
                return false;

            var today = DateTime.Today;
            var minDate = today.AddYears(-6); // Ngày nhỏ nhất được chấp nhận là 6 năm trước từ ngày hiện tại

            // Ngày sinh phải trước ngày minDate và không được trong tương lai
            return birthday.Value <= today && birthday.Value <= minDate;
        }

        private string FormatFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return string.Empty;

            // Tách tên đầy đủ thành các phần từ và xử lý từng phần
            var nameParts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var formattedParts = nameParts.Select(part =>
                char.ToUpper(part[0]) + part.Substring(1).ToLower()
            );
            return string.Join(" ", formattedParts);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                var roles = await _UserBusiness.GetRoles();
                if (roles != null)
                {
                    ViewData["Roles"] = roles.Select(u => new SelectListItem
                    {
                        Value = u.RoleId.ToString(),
                        Text = u.RoleName
                    }).ToList();
                }
                else
                {
                    ViewData["Roles"] = new List<SelectListItem>();
                }
                //_UserBusiness.Update(User);                
                //return Page();
            }

            // Định dạng tên đầy đủ
            User.Fullname = FormatFullName(User.Fullname);

            // Loại bỏ khoảng trắng dư thừa trong số điện thoại trước khi cập nhật
            if (!string.IsNullOrEmpty(User.Phone))
            {
                User.Phone = User.Phone.Trim();
            }

            if (!ValidateUsername(User.Username))
            {
                ModelState.AddModelError(string.Empty, "Username must be at least 7 characters long and cannot contain spaces.");
                var roles = await _UserBusiness.GetRoles();
                if (roles != null)
                {
                    ViewData["Roles"] = roles.Select(u => new SelectListItem
                    {
                        Value = u.RoleId.ToString(),
                        Text = u.RoleName
                    }).ToList();
                }
                else
                {
                    ViewData["Roles"] = new List<SelectListItem>();
                }
                return Page();
            }

            if (!ValidatePassword(User.Password))
            {
                ModelState.AddModelError(string.Empty, "Password must be at least 12 characters long, contain at least one uppercase letter, one lowercase letter, one digit, one special character and no whitespace characters.");
                var roles = await _UserBusiness.GetRoles();
                if (roles != null)
                {
                    ViewData["Roles"] = roles.Select(u => new SelectListItem
                    {
                        Value = u.RoleId.ToString(),
                        Text = u.RoleName
                    }).ToList();
                }
                else
                {
                    ViewData["Roles"] = new List<SelectListItem>();
                }
                return Page();
            }

            if (!Validation.IsValidEmail(User.Email))
            {
                ModelState.AddModelError(string.Empty, "Invalid email format.");
                var roles = await _UserBusiness.GetRoles();
                if (roles != null)
                {
                    ViewData["Roles"] = roles.Select(u => new SelectListItem
                    {
                        Value = u.RoleId.ToString(),
                        Text = u.RoleName
                    }).ToList();
                }
                else
                {
                    ViewData["Roles"] = new List<SelectListItem>();
                }
                return Page();
            }

            if (!Validation.IsValidPhoneNumber(User.Phone))
            {
                ModelState.AddModelError(string.Empty, "Invalid phone number format. Phone number must be between 10 to 15 digits.");
                var roles = await _UserBusiness.GetRoles();
                if (roles != null)
                {
                    ViewData["Roles"] = roles.Select(u => new SelectListItem
                    {
                        Value = u.RoleId.ToString(),
                        Text = u.RoleName
                    }).ToList();
                }
                else
                {
                    ViewData["Roles"] = new List<SelectListItem>();
                }
                return Page();
            }

            if (!ValidateBirthday(User.Birthday))
            {
                ModelState.AddModelError(string.Empty, "Birthday must be at least 6 years ago and cannot be in the future.");
                var roles = await _UserBusiness.GetRoles();
                if (roles != null)
                {
                    ViewData["Roles"] = roles.Select(u => new SelectListItem
                    {
                        Value = u.RoleId.ToString(),
                        Text = u.RoleName
                    }).ToList();
                }
                else
                {
                    ViewData["Users"] = new List<SelectListItem>();
                }
                return Page();
            }

            var result = await _UserBusiness.Update(User);
            if (result.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                var roles = await _UserBusiness.GetRoles();
                ViewData["Roles"] = roles.Select(u => new SelectListItem
                {
                    Value = u.RoleId.ToString(),
                    Text = u.RoleName
                }).ToList();
                return Page();
            }

            //return RedirectToPage("./Index");
        }

        //private bool UserExists(int id)
        //{
        //  return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        //}
    }
}
