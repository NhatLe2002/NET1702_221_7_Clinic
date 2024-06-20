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

            var user = await _UserBusiness.GetById(id.ToString());

            if (user != null && user.Data is User userReturn)
            {
                User = userReturn;
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
                return Page();
            }
            _UserBusiness.Update(User);

            return RedirectToPage("./Index");
        }

        //private bool UserExists(int id)
        //{
        //  return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        //}
    }
}
