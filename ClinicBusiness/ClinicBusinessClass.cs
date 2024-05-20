using ClinicBusiness.Base;
using ClinicData.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public class ClinicBusinessClass
    {
        private readonly ClinicDAO _clinicDAO;


        public ClinicBusinessClass()
        {
            _clinicDAO = new ClinicDAO();
        }

        public async Task<List<IBusinessResult>> GetAllAsync()
        {
            try
            {
                var clinics = await _clinicDAO.GetAllAsync();
                if (clinics != null && clinics.Count > 0)
                {
                    List<IBusinessResult> results = new List<IBusinessResult>();
                    foreach (var clinic in clinics)
                    {
                        results.Add(new BusinessResult
                        {
                            Data = clinic,
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
