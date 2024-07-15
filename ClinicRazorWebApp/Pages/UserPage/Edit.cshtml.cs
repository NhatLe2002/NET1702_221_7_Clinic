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
