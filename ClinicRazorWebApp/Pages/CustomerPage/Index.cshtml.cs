using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicData.Models;
using ClinicBusiness;
using ClinicCommon;
using System.Drawing.Printing;

namespace ClinicRazorWebApp.Pages.CustomerPage
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerBusinessClass _customerBusiness;

        public IList<Customer> Customer { get; set; } = default!;
        public int PageSize { get; set; } = 3;
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalCustomer { get; set; } = 20;
        //Search
        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Address { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Phone { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; }
        public IndexModel(ICustomerBusinessClass customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        public async Task OnGetAsync(int? pageIndex)
        {
            PageIndex = pageIndex ?? 1;
            List<Customer> pagedCustomers;
            List<Customer> customerResult = (List<Customer>)((await _customerBusiness.GetAll())?.Data);
            if (!string.IsNullOrEmpty(Name))
            {
                customerResult = customerResult.Where(c => c.CustomerName.Contains(Name)).ToList();
            }
            if (!string.IsNullOrEmpty(Address))
            {
                customerResult = customerResult.Where(c => c.Address.Contains(Address)).ToList();
            }
            if (!string.IsNullOrEmpty(Phone))
            {
                customerResult = customerResult.Where(c => c.Phone.Contains(Phone)).ToList();
            }
            if (!string.IsNullOrEmpty(Email))
            {
                customerResult = customerResult.Where(c => c.Email.Contains(Email)).ToList();
            }
            pagedCustomers = PaginationHelper.GetPaged(customerResult, PageIndex, PageSize);
            //Handle when delete customer and page no any customer
            if ((pagedCustomers == null || pagedCustomers.Count == 0) && pageIndex - 1 > 0)
            {
                PageIndex--;
                pagedCustomers = PaginationHelper.GetPaged(customerResult, PageIndex, PageSize);
            }

            TotalCustomer = customerResult.Count;
            TotalPages = (int)Math.Ceiling(customerResult.Count / (double)PageSize);
            Customer = pagedCustomers;
        }

        public async Task<IActionResult> OnPostDeleteACustomer(int id, int pageIndex)
        {
            try
            {
                var result = await _customerBusiness.DeleteById(id.ToString());
                if (result.Status == Const.SUCCESS_DELETE_CODE)
                {
                    await OnGetAsync(pageIndex);
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to delete customer.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error deleting customer: {ex.Message}");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAllCustomer()
        {
            try
            {
                var result = await _customerBusiness.DeleteAllAsync();
                if (result.Status == Const.SUCCESS_DELETE_CODE)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to delete customer.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error deleting customer: {ex.Message}");
                return Page();
            }
        }
    }
}
