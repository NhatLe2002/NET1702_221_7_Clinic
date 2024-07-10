using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;

namespace ClinicRazorWebApp.Pages.ExaminationResultPage
{
    public class EditModel : PageModel
    {
        private readonly ClinicData.Models.NET1702_PRN221_ClinicContext _context;

        public EditModel(ClinicData.Models.NET1702_PRN221_ClinicContext context)
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

            var examinationresult =  await _context.ExaminationResults.FirstOrDefaultAsync(m => m.ExaminationResultId == id);
            if (examinationresult == null)
            {
                return NotFound();
            }
            ExaminationResult = examinationresult;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ExaminationResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExaminationResultExists(ExaminationResult.ExaminationResultId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ExaminationResultExists(int id)
        {
            return _context.ExaminationResults.Any(e => e.ExaminationResultId == id);
        }
    }
}
