using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;

namespace ClinicRazorWebApp.Pages.ExaminationResultPage
{
    public class IndexModel : PageModel
    {
        private readonly ClinicData.Models.NET1702_PRN221_ClinicContext _context;

        public IndexModel(ClinicData.Models.NET1702_PRN221_ClinicContext context)
        {
            _context = context;
        }

        public IList<ExaminationResult> ExaminationResult { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ExaminationResult = await _context.ExaminationResults.ToListAsync();
        }
    }
}
