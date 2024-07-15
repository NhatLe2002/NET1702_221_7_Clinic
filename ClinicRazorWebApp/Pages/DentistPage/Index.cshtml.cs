using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicData.Models;
using ClinicBusiness;
using ClinicCommon;

namespace ClinicRazorWebApp.Pages.DentistPage
{
    public class IndexModel : PageModel
    {
        private readonly IDentistBusiness _dentistBusiness;
        private readonly IClinicBusinessClass _clinicBusiness;

        public IndexModel(IDentistBusiness dentistBusiness, IClinicBusinessClass clinicBusiness)
        {
            _dentistBusiness = dentistBusiness;
            _clinicBusiness = clinicBusiness;
        }

        public IList<Dentist> Dentist { get; set; }
        public string SearchText { get; set; }
        public string SearchPhone { get; set; }
        public int SelectedClinicId { get; set; }
        public SelectList Clinics { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task OnGetAsync(string searchText, string searchPhone, int selectedClinicId = 0, int pageIndex = 1)
        {
            SearchText = searchText;
            SearchPhone = searchPhone;
            SelectedClinicId = selectedClinicId;
            PageIndex = pageIndex;

            await LoadDataAsync();
        }

        public async Task<IActionResult> OnPostSearchAsync(string searchText, string searchPhone, int selectedClinicId = 0)
        {
            return RedirectToPage(new { searchText, searchPhone, selectedClinicId, pageIndex = 1 });
        }

        private async Task LoadDataAsync()
        {
            var result = await _clinicBusiness.GetAll();
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                var clinics = ((List<Clinic>)result.Data).Select(c => new SelectListItem
                {
                    Value = c.ClinicId.ToString(),
                    Text = c.ClinicName
                }).ToList();
                Clinics = new SelectList(clinics, "Value", "Text");
            }

            var dentistResult = await _dentistBusiness.GetAll();
            if (dentistResult.Status > 0 && dentistResult.Data != null)
            {
                var dentists = (List<Dentist>)dentistResult.Data;
                if (!string.IsNullOrEmpty(SearchText))
                {
                    dentists = dentists.Where(d =>
                        d.DentistName.ToLower().Contains(SearchText.ToLower()) ||
                        d.Clinic.ClinicName.ToLower().Contains(SearchText.ToLower())
                    ).ToList();
                }
                if (!string.IsNullOrEmpty(SearchPhone))
                {
                    dentists = dentists.Where(d =>
                        d.Phone.ToLower().Contains(SearchPhone.ToLower())
                    ).ToList();
                }
                if (SelectedClinicId > 0)
                {
                    dentists = dentists.Where(d => d.ClinicId == SelectedClinicId).ToList();
                }

                TotalPages = PaginationHelper.GetTotalPages(dentists, PageSize);

                Dentist = PaginationHelper.GetPaged(dentists, PageIndex, PageSize);
            }
            else
            {
                Dentist = new List<Dentist>();
                TotalPages = 0;
            }
        }
    }
}
