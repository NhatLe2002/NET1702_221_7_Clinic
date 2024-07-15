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
        private const int PageSize = 5;

        public IndexModel(IUserBusiness userBusiness)
        {
            _UserBusiness = userBusiness;
        }

        public IList<User> User { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchField { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageIndex = 1, string searchTerm = null, string searchField = null)
        {
            PageIndex = pageIndex;
            SearchTerm = searchTerm;
            SearchField = searchField;
            try
            {
                var userResult = await _UserBusiness.GetAllUserAsync();
                if (userResult.Status == Const.SUCCESS_READ_CODE)
                {
                    var users = userResult.Data as List<User>;
                    if (users != null)
                    {
                        if (!string.IsNullOrEmpty(SearchTerm) && !string.IsNullOrEmpty(SearchField))
                        {
                            users = SearchUsers(users, SearchField, SearchTerm);
                        }

                        // Tính toán tổng số trang và lấy dữ liệu của trang hiện tại
                        TotalPages = PaginationHelper.GetTotalPages(users, PageSize);
                        User = PaginationHelper.GetPaged(users, PageIndex, PageSize);

                        // Log các giá trị để kiểm tra
                        Console.WriteLine($"TotalPages: {TotalPages}, PageIndex: {PageIndex}, User Count: {User.Count}");
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
                // Log hoặc xử lý trường hợp không thành công
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
                case "Role":
                    users = users.Where(u => u.Role.RoleName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
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
