using ClinicBusiness.Base;
using ClinicData.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public class ClinicBusiness
    {
        private readonly ClinicDAO _clinicDAO;
      

        public ClinicBusiness() 
        {
            _clinicDAO = new ClinicDAO();
        }

        public async Task<List<IBusinessResult>> GetAllAsync()
        {
            try
            {
                var clinic = await _clinicDAO.GetAllAsync();
                BusinessResult result = new BusinessResult
                    {
                    Data = clinic,
                    Message = "Oke",
                    Status = 2,
                };
                return new List<IBusinessResult> { result };
            }catch (Exception ex) {
            
                return new List<IBusinessResult> {  new BusinessResult { 
                    Status = 4,
                    Message = ex.Message,
                } };
            }
        }
    }
}
