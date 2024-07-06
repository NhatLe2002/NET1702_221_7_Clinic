using ClinicBusiness.Base;
using ClinicCommon;
using ClinicData;
using ClinicData.DAO;
using ClinicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public interface IClinicBusinessClass
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Save(Clinic clinic);
        Task<IBusinessResult> Update(Clinic clinic);
        Task<IBusinessResult> DeleteById(string code);
    }
    public class ClinicBusinessClass : IClinicBusinessClass
    {
        private readonly UnitOfWork _unitOfWork;


        public ClinicBusinessClass()
        {
            _unitOfWork = new UnitOfWork();
        }



        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                //var currencies = await _currencyRepository.GetAllAsync();
                var currencies = await _unitOfWork.ClinicRepository.GetAllAsync();


                if (currencies == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, currencies);
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
                var clinics = await _unitOfWork.ClinicRepository.GetAllAsync();
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

        public async Task<IBusinessResult> GetById(string code)
        {
            try
            {
                #region Business rule
                #endregion

                //var currency = await _currencyRepository.GetByIdAsync(code);
                var clinic = await _unitOfWork.ClinicRepository.GetByIdAsync(code);

                if (clinic == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, clinic);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Clinic currency)
        {
            try
            {
                //int result = await _currencyRepository.CreateAsync(currency);
                int result = await _unitOfWork.ClinicRepository.CreateAsync(currency);
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

        public async Task<IBusinessResult> Update(Clinic currency)
        {
            try
            {
                //int result = await _currencyRepository.UpdateAsync(currency);
                int result = await _unitOfWork.ClinicRepository.UpdateAsync(currency);

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
                //var currency = await _currencyRepository.GetByIdAsync(code);
                var currency = await _unitOfWork.ClinicRepository.GetByIdAsync(code);
                if (currency != null)
                {
                    //var result = await _currencyRepository.RemoveAsync(currency);
                    var result = await _unitOfWork.ClinicRepository.RemoveAsync(currency);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, currency);
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
        public async Task<List<IBusinessResult>> GetAllByFillterAndPagingAsync(string searchClinicName, string address, string phone, int? minDentists, int? maxDentists, int? pageIndex, int? pageSize)
        {
            try
            {
                var clinics = _unitOfWork.ClinicRepository.GetAllByFillterAndPaging(
               filter: c => (string.IsNullOrEmpty(searchClinicName) || c.ClinicName.Contains(searchClinicName)) &&
                            (string.IsNullOrEmpty(address) || c.Address.Contains(address)) &&
                            (string.IsNullOrEmpty(phone) || c.Phone.Contains(phone)),
               orderBy: clinics => clinics.OrderBy(c => c.ClinicName),
               pageIndex: pageIndex ?? 1,
               pageSize: pageSize ?? 10
           );
                if (clinics != null )
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

