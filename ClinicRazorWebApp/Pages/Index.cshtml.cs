using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ClinicRazorWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        [Required(ErrorMessage = "User name is required.")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var session = HttpContext.Session;
            if (_configuration.GetSection("Login")["Username"].Equals(Username) &&
                _configuration.GetSection("Login")["Password"].Equals(Password))
            {
                ClinicRazorWebApp.SessionExtendsions.Set<string>(session, "User", Username);

                return RedirectToPage("/CustomerPage/Index");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Thông tin đăng nhập không đúng.");
                ViewData["ErrorMessage"] = "Thông tin đăng nhập không đúng.";
                return Page();
            }
        }
    }
}