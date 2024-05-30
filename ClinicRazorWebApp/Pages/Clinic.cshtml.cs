using ClinicBusiness;
using ClinicData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicRazorWebApp.Pages
{
    public class ClinicModel : PageModel
    {
        private readonly IClinicBusinessClass _ClinicBusiness = new ClinicBusinessClass();
        public string Message { get; set; } = default;
        [BindProperty]
        public Clinic Clinic { get; set; } = default;
        public List<Clinic> Clinics { get; set; } = new List<Clinic>();
        public void OnGet()
        {
            Clinics = this.GetCurrencies();
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


