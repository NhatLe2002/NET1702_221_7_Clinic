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
    public interface IUserBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Save(User user);
        Task<IBusinessResult> Update(User user);
        Task<IBusinessResult> DeleteById(string code);
        Task<IEnumerable<Role>> GetRoles();
        Task<IBusinessResult> GetAllUserAsync();
    }
    public class UserBusiness : IUserBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public UserBusiness()
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
                var users = await _unitOfWork.UserRepository.GetAllAsync();


                if (users == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, users);
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
                var users = await _unitOfWork.UserRepository.GetAllAsync();
                if (users != null && users.Count > 0)
                {
                    List<IBusinessResult> results = new List<IBusinessResult>();
                    foreach (var user in users)
                    {
                        results.Add(new BusinessResult
                        {
                            Data = user,
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
                var currency = await _unitOfWork.UserRepository.GetByIdAsync(code);

                if (currency == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, currency);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<IBusinessResult> Save(User currency)
        {
            try
            {
                //int result = await _currencyRepository.CreateAsync(currency);
                int result = await _unitOfWork.UserRepository.CreateAsync(currency);
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
        public async Task<IBusinessResult> Update(User currency)
        {
            try
            {
                //int result = await _currencyRepository.UpdateAsync(currency);
                int result = await _unitOfWork.UserRepository.UpdateAsync(currency);

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
                var currency = await _unitOfWork.UserRepository.GetByIdAsync(code);
                if (currency != null)
                {
                    //var result = await _currencyRepository.RemoveAsync(currency);
                    var result = await _unitOfWork.UserRepository.RemoveAsync(currency);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
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

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _unitOfWork.RoleRepository.GetAllAsync();
        }

        public async Task<IBusinessResult> GetAllUserAsync()
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAllUserAsync();
                if (users != null && users.Count > 0)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, users);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG, null);
                }
                //return new Result<List<User>>(users, Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in GetAllUserAsync: {ex.Message}");
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message, null);
                //return new Result<List<User>>(null, Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
