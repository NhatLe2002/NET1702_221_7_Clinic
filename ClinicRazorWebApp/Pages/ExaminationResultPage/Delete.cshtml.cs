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
    public class DeleteModel : PageModel
    {
        private readonly ClinicData.Models.NET1702_PRN221_ClinicContext _context;

        public DeleteModel(ClinicData.Models.NET1702_PRN221_ClinicContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExaminationResult ExaminationResult { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examinationresult = await _context.ExaminationResults.FirstOrDefaultAsync(m => m.ExaminationResultId == id);

            if (examinationresult == null)
            {
                return NotFound();
            }
            else
            {
                ExaminationResult = examinationresult;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examinationresult = await _context.ExaminationResults.FindAsync(id);
            if (examinationresult != null)
            {
                ExaminationResult = examinationresult;
                _context.ExaminationResults.Remove(ExaminationResult);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
