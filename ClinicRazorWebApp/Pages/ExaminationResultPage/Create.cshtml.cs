using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicData.Models;

namespace ClinicRazorWebApp.Pages.ExaminationResultPage
{
    public class CreateModel : PageModel
    {
        private readonly ClinicData.Models.NET1702_PRN221_ClinicContext _context;

        public CreateModel(ClinicData.Models.NET1702_PRN221_ClinicContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ExaminationResult ExaminationResult { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ExaminationResults.Add(ExaminationResult);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
