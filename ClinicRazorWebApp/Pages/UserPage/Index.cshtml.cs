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
            var userResult = await _UserBusiness.GetAll();
            if (userResult.Status == Const.SUCCESS_READ_CODE)
            {
                User = userResult.Data as IList<User>;
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
