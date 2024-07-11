using ClinicBusiness.Base;
using ClinicCommon;
using ClinicData.Models;
using ClinicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public interface IDentistBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Save(Dentist Dentist);
        Task<IBusinessResult> Update(Dentist Dentist);
        Task<IBusinessResult> DeleteById(string code);
    }
    public class DentistBusinessClass : IDentistBusiness
    {
        private readonly UnitOfWork _unitOfWork;


        public DentistBusinessClass()
        {
            _unitOfWork = new UnitOfWork();
        }



        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var dentists = _DAO.GetAll();
                //var dentists = await _dentistRepository.GetAllAsync();
                var dentists = await _unitOfWork.DentistRepository.GetAllWithDetailsAsync();


                if (dentists == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, dentists);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<List<IBusinessResult>> GetAllAsync()
        {
            try
            {
                var Dentists = await _unitOfWork.DentistRepository.GetAllAsync();
                if (Dentists != null && Dentists.Count > 0)
                {
                    List<IBusinessResult> results = new List<IBusinessResult>();
                    foreach (var Dentist in Dentists)
                    {
                        results.Add(new BusinessResult
                        {
                            Data = Dentist,
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

        public async Task<IBusinessResult> GetById(string code)
        {
            try
            {
                #region Business rule
                #endregion

                //var dentist = await _dentistRepository.GetByIdAsync(code);
                var dentists = await _unitOfWork.DentistRepository.GetByIdWithDetailsAsync(code);

                if (dentists == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, dentists);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Dentist dentist)
        {
            try
            {
                int result = await _unitOfWork.DentistRepository.CreateAsync(dentist);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(Dentist dentist)
        {
            try
            {
                //int result = await _dentistRepository.UpdateAsync(dentist);
                int result = await _unitOfWork.DentistRepository.UpdateAsync(dentist);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }
        public async Task<IBusinessResult> DeleteById(string code)
        {
            try
            {
                //var dentist = await _dentistRepository.GetByIdAsync(code);
                var dentist = await _unitOfWork.DentistRepository.GetByIdAsync(code);
                if (dentist != null)
                {
                    //var result = await _dentistRepository.RemoveAsync(dentist);
                    var result = await _unitOfWork.DentistRepository.RemoveAsync(dentist);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, dentist);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }
    }
}
