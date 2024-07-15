﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicData.Models;
using ClinicBusiness;
using ClinicCommon;
using Microsoft.AspNetCore.Http;

namespace ClinicRazorWebApp.Pages.DentistPage
{
    public class EditModel : PageModel
    {
        private readonly IDentistBusiness _dentistBusiness;
        private readonly IClinicBusinessClass _clinicBusiness;
        private readonly ICommonService _commonService;

        [BindProperty]
        public Dentist Dentist { get; set; } = default!;
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public List<Clinic> Clinics { get; set; } = new List<Clinic>();

        public EditModel(IDentistBusiness dentistBusiness, IClinicBusinessClass clinicBusiness, ICommonService commonService)
        {
            _dentistBusiness = dentistBusiness;
            _clinicBusiness = clinicBusiness;
            _commonService = commonService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dentistResponse = await _dentistBusiness.GetById(id.ToString());
            if (dentistResponse == null || dentistResponse.Data == null)
            {
                return NotFound();
            }

            Dentist = dentistResponse.Data as Dentist;
            if (Dentist == null)
            {
                return NotFound();
            }

            var result = await _clinicBusiness.GetAll();
            if (result.Status != Const.SUCCESS_READ_CODE)
            {
                ModelState.AddModelError(string.Empty, "Error fetching clinics: " + result.Message);
                return Page();
            }

            var clinics = result.Data as List<Clinic>;
            if (clinics == null)
            {
                return Page();
            }

            ViewData["ClinicId"] = new SelectList(clinics.Select(clinic => new SelectListItem
            {
                Value = clinic.ClinicId.ToString(),
                Text = clinic.ClinicName
            }).ToList(), "Value", "Text");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var result = await _clinicBusiness.GetAll();
                if (result.Status != Const.SUCCESS_READ_CODE)
                {
                    ModelState.AddModelError(string.Empty, "Error fetching clinics: " + result.Message);
                    return Page();
                }

                var clinics = result.Data as List<Clinic>;
                if (clinics == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid clinic data received.");
                    return Page();
                }

                ViewData["ClinicId"] = new SelectList(clinics.Select(clinic => new SelectListItem
                {
                    Value = clinic.ClinicId.ToString(),
                    Text = clinic.ClinicName
                }).ToList(), "Value", "Text");

                //return Page();
            }

            try
            {
                if (ImageFile != null)
                {
                    var imageUrl = await _commonService.UploadAnImage(ImageFile, "dentists", Dentist.DentistName);
                    Dentist.Image = imageUrl;
                }

                var updateResult = await _dentistBusiness.Update(Dentist);
                if (updateResult.Status != Const.SUCCESS_UPDATE_CODE)
                {
                    ModelState.AddModelError(string.Empty, "Error updating Dentist: " + updateResult.Message);
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred: " + ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}