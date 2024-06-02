using ClinicBusiness;
using ClinicData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicRazorWebApp.Pages
{
    public class UserModel : PageModel
    {
        private readonly IUserBusiness _UserBusiness = new UserBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public User User { get; set; } = default;
        public List<User> Users { get; set; } = new List<User>();
        public void OnGet()
        {
        }
    }
}
