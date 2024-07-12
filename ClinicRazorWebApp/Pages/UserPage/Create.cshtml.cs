using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicData.Models;
using ClinicBusiness;
using ClinicCommon;
using CloudinaryDotNet.Actions;

namespace ClinicRazorWebApp.Pages.UserPage
{
    public class CreateModel : PageModel
    {
        private readonly IUserBusiness _UserBusiness;

        public CreateModel(IUserBusiness userBusiness)
        {
            _UserBusiness = userBusiness;
        }
        //private readonly ClinicData.Models.NET1702_PRN221_ClinicContext _context;

        /*public CreateModel(ClinicData.Models.NET1702_PRN221_ClinicContext context)
        {
            _context = context;
        }*/

        public async Task<IActionResult> OnGetAsync()
        {
            //ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
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

        [BindProperty]
        public User User { get; set; } = default!;

        private bool ValidatePassword(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 6;
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || _context.Users == null || User == null)
            if (!ModelState.IsValid)
            {
                //var roles = await _UserBusiness.GetRoles();
                //ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
                //return Page();
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
            }

            if (!ValidatePassword(User.Password))
            {
                ModelState.AddModelError(string.Empty, "Password must be at least 6 characters long.");
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

            //_context.Users.Add(User);
            //await _context.SaveChangesAsync();
            var userResult = await _UserBusiness.Save(User);
            if (userResult.Status == Const.SUCCESS_CREATE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, userResult.Message);
                var roles = await _UserBusiness.GetRoles();
                ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
                return Page();
            }
        }
    }
}
