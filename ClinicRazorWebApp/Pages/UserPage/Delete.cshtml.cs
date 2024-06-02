using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;

namespace ClinicRazorWebApp.Pages.UserPage
{
    public class DeleteModel : PageModel
    {
        private readonly IUserBusiness _UserBusiness;

        public DeleteModel(IUserBusiness userBusiness)
        {
            _UserBusiness = userBusiness;
        }
        //private readonly ClinicData.Models.NET1702_PRN221_ClinicContext _context;

        /*public DeleteModel(ClinicData.Models.NET1702_PRN221_ClinicContext context)
        {
            _context = context;
        }*/

        [BindProperty]
      public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //if (id == null || _context.Users == null)
            //{
            //    return NotFound();
            //}
            if (id == null)
            {
                return NotFound();
            }

            //var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            var user = await _UserBusiness.GetById(id.ToString());

            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                //User = user;
                if (user.Data is User userResult)
                {

                    User = userResult;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //if (id == null || _context.Users == null)
            //{
            //    return NotFound();
            //}
            _UserBusiness.DeleteById(id.ToString());
            //var user = await _context.Users.FindAsync(id);

            //if (user != null)
            //{
            //    User = user;
            //    _context.Users.Remove(User);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage("./Index");
        }
    }
}
