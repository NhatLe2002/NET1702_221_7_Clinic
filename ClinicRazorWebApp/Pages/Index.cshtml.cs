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
            Password = Password.Trim();
            if(Password.Length < 8)
            {
                ViewData["ErrorMessage"] = "Password is more or equal than 8 characters!";
                return Page();
            }
            var session = HttpContext.Session;
            if (_configuration.GetSection("Login")["Username"].Equals(Username) &&
                _configuration.GetSection("Login")["Password"].Equals(Password))
            {
                ClinicRazorWebApp.SessionExtendsions.Set<string>(session, "User", Username);

                return RedirectToPage("/CustomerPage/Index");
            }
            else
            {
                ViewData["ErrorMessage"] = "Username or password is incorrect!";
                return Page();
            }
        }
    }
}