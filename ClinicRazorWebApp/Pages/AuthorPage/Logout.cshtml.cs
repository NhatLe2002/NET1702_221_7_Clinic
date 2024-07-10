using ClinicData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicRazorWebApp.Pages.AuthorPage
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            ClinicRazorWebApp.SessionExtendsions.Set<string>(HttpContext.Session, "User", null);
            return RedirectToPage("/Index");
        }
    }
}
