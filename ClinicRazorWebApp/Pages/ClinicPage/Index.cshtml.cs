using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;

namespace ClinicRazorWebApp.Pages.ClinicPage
{
    public class IndexModel : PageModel
    {
        private readonly IClinicBusinessClass _ClinicBusiness ;

        public IndexModel(IClinicBusinessClass clinicBusinessClass)
        {
            _ClinicBusiness = clinicBusinessClass ;
        }

        public IList<Clinic> Clinic { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Clinic = this.GetCurrencies();
            /* if (_context.Clinics != null)
             {
                 Clinic = await _context.Clinics.ToListAsync();
             }*/
        }

        private List<Clinic> GetCurrencies()
        {
            var currencyResult = _ClinicBusiness.GetAll();

            if (currencyResult.Status > 0 && currencyResult.Result.Data != null)
            {
                var currencies = (List<Clinic>)currencyResult.Result.Data;
                return currencies;
            }
            return new List<Clinic>();
        }
    }
}
