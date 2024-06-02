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
    public class IndexModel : PageModel
    {
        private readonly IUserBusiness _UserBusiness;

        public IndexModel(IUserBusiness userBusiness)
        {
            _UserBusiness = userBusiness;
        }

        //private readonly ClinicData.Models.NET1702_PRN221_ClinicContext _context;

        /*public IndexModel(ClinicData.Models.NET1702_PRN221_ClinicContext context)
        {
            _context = context;
        }*/

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = this.GetCurrencies();
            /*if (_context.Users != null)
            {
                User = await _context.Users
                .Include(u => u.Role).ToListAsync();
            }*/
        }
        private List<User> GetCurrencies()
        {
            var currencyResult = _UserBusiness.GetAll();

            if (currencyResult.Status > 0 && currencyResult.Result.Data != null)
            {
                var currencies = (List<User>)currencyResult.Result.Data;
                return currencies;
            }
            return new List<User>();
        }
    }
}
