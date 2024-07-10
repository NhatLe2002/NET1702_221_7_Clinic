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

        public async Task OnGetAsync()
        {
            try
            {
                var userResult = await _UserBusiness.GetAllUserAsync();
                if (userResult.Status == Const.SUCCESS_READ_CODE)
                {
                    User = userResult.Data as List<User>;
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
