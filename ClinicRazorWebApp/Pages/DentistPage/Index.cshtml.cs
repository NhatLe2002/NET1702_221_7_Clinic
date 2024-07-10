using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;
using DentistBusiness;

namespace ClinicRazorWebApp.Pages.DentistPage
{
    public class IndexModel : PageModel
    {
        
        

        //public async Task OnGetAsync()
        //{
        //    if (_context.Dentists != null)
        //    {
        //        Dentist = await _context.Dentists
        //        .Include(d => d.Clinic)
        //        .Include(d => d.User).ToListAsync();
        //    }
        //}
        private readonly IDentistBusiness _dentistBusiness;

        public IndexModel(IDentistBusiness dentistBusiness)
        {
            _dentistBusiness = dentistBusiness;
        }

        public IList<Dentist> Dentist { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Dentist = this.GetDentists();
        }
        private List<Dentist> GetDentists()
        {
            var DentistResult = _dentistBusiness.GetAll();

            if (DentistResult.Status > 0 && DentistResult.Result.Data != null)
            {
                var dentists = (List<Dentist>)DentistResult.Result.Data;
                return dentists;
            }
            return new List<Dentist>();
        }
    }
}
