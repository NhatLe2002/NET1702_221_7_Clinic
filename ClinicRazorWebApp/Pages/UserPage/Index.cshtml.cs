using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;
using ClinicCommon;

namespace ClinicRazorWebApp.Pages.UserPage
{
    public class IndexModel : PageModel
    {
        private readonly IUserBusiness _UserBusiness;

        public IndexModel(IUserBusiness userBusiness)
        {
            _UserBusiness = userBusiness;
        }

        public IList<User> User { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchField { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var userResult = await _UserBusiness.GetAllUserAsync();
                if (userResult.Status == Const.SUCCESS_READ_CODE)
                {
                    User = userResult.Data as List<User>;
                    if (!string.IsNullOrEmpty(SearchTerm) && !string.IsNullOrEmpty(SearchField))
                    {
                        User = SearchUsers(User.ToList(), SearchField, SearchTerm);
                    }
                    Console.WriteLine(Const.SUCCESS_READ_MSG); // Print "Get data success"
                }
                else
                {
                    // Log hoặc xử lý trường hợp không thành công
                    Console.WriteLine($"Failed to load user list: {userResult.Message}");
                }
            }
            catch (Exception ex)
            {
                // Log or handle unsuccessful case
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
        }

        private List<User> SearchUsers(List<User> users, string searchField, string searchTerm)
        {
            switch (searchField)
            {
                case "Fullname":
                    users = users.Where(u => u.Fullname.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "Email":
                    users = users.Where(u => u.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "Phone":
                    if (int.TryParse(searchTerm, out int phone))
                    {
                        users = users.Where(u => u.Phone.ToString().Contains(phone.ToString())).ToList();
                    }
                    break;
                    break;
                case "Username":
                    users = users.Where(u => u.Username.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "Address":
                    users = users.Where(u => u.Address.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "Birthday":
                    if (DateTime.TryParse(searchTerm, out DateTime birthday))
                    {
                        users = users.Where(u => u.Birthday.HasValue && u.Birthday.Value.Date == birthday.Date).ToList();
                    }
                    break;
                case "IsActive":
                    if (bool.TryParse(searchTerm, out bool isActive))
                    {
                        users = users.Where(u => u.IsActive == isActive).ToList();
                    }
                    break;
                default:
                    break;
            }
            return users;
        }

        /*public async Task OnGetAsync()
        {
            User = this.GetUsers();
        }
        private List<User> GetUsers()
        {
            var userResult = _UserBusiness.GetAll();

            if (userResult.Status > 0 && userResult.Result.Data != null)
            {
                var currencies = (List<User>)userResult.Result.Data;
                return currencies;
            }
            return new List<User>();
        }*/
    }
}
