using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;

namespace ClinicRazorWebApp.Pages.ClinicUpdatePage
{
    public class IndexModel : PageModel
    {
        private readonly IClinicBusinessClass _ClinicBusiness;

        public IndexModel(IClinicBusinessClass clinicBusinessClass)
        {
            _ClinicBusiness = clinicBusinessClass;
        }

        public IList<Clinic> Clinic { get; set; } = default!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3; // You can adjust this as needed
        public int TotalPages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchClinicName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchAddress { get; set; }
        [BindProperty(SupportsGet = true)]
        public TimeSpan? SearchOpenTime { get; set; }
        [BindProperty(SupportsGet = true)]
        public TimeSpan? SearchCloseTime { get; set; }
        public async Task OnGetAsync(int pageNumber = 1)
        {
            PageNumber = pageNumber;
            var currencyResult = await _ClinicBusiness.GetAll();

            if (currencyResult.Status > 0 && currencyResult.Data != null)
            {
                var clinics = (List<Clinic>)currencyResult.Data;

                // Filter by IsActive
                clinics = clinics.Where(c => c.IsActive == true).ToList();

                // Apply search filters
                if (!string.IsNullOrEmpty(SearchClinicName))
                {
                    clinics = clinics.Where(c => c.ClinicName.Contains(SearchClinicName, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (!string.IsNullOrEmpty(SearchAddress))
                {
                    clinics = clinics.Where(c => c.Address.Contains(SearchAddress, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (SearchOpenTime.HasValue)
                {
                    clinics = clinics.Where(c => c.OpenTime.HasValue && c.OpenTime.Value >= SearchOpenTime.Value).ToList();
                }
                if (SearchCloseTime.HasValue)
                {
                    clinics = clinics.Where(c => c.CloseTime.HasValue && c.CloseTime.Value <= SearchCloseTime.Value).ToList();
                }

                // Pagination
                TotalPages = (int)Math.Ceiling(clinics.Count / (double)PageSize);
                Clinic = clinics.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
            }
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
