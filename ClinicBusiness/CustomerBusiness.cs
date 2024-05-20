using ClinicBusiness.Base;
using ClinicData.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public class CustomerBusiness
    {
        private CustomerDAO _customerDao;

        public CustomerBusiness()
        {
            _customerDao = new CustomerDAO();
        }
        public async Task<List<IBusinessResult>> GetAllAsync()
        {
            try
            {
                var customerList = await _customerDao.GetAllAsync();
                if (customerList != null && customerList.Count > 0)
                {
                    List<IBusinessResult> results = new List<IBusinessResult>();
                    foreach (var customer in customerList)
                    {
                        results.Add(new BusinessResult
                        {
                            Data = customer,
                            Message = "Oke",
                            Status = 2
                        });
                    }
                    return results;
                }
                else
                {
                    return new List<IBusinessResult>();
                }
            }
            catch (Exception ex)
            {

                return new List<IBusinessResult> {  new BusinessResult {
                    Status = 4,
                    Message = ex.Message,
                } };
            }
        }
    }
}
