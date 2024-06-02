using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicData.Models;
using ClinicBusiness;

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

        public IActionResult OnGet()
        {
        //ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || _context.Users == null || User == null)
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Users.Add(User);
            //await _context.SaveChangesAsync();
            var userResult = _UserBusiness.Save(User);
            return RedirectToPage("./Index");
        }
    }
}
