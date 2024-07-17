using ClinicBusiness.Base;
using ClinicCommon;
using ClinicData;
using ClinicData.DAO;
using ClinicData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public interface ICustomerBusinessClass
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Save(Customer customer);
        Task<IBusinessResult> Update(Customer customer);
        Task<IBusinessResult> DeleteById(string code);
        Task<IBusinessResult> DeleteAllAsync();
        Task<IBusinessResult> Notification();
    }

    public class CustomerBusiness : ICustomerBusinessClass
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ICommonService _commonService;

        public CustomerBusiness(ICommonService commonService)
        {
            _unitOfWork = new UnitOfWork();
            _commonService = commonService;
        }



        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                //var currencies = await _currencyRepository.GetAllAsync();
                var customers = await _unitOfWork.CustomerRepository.GetAllAsync();


                if (customers == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customers);
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
                var customer = await _unitOfWork.CustomerRepository.GetAllAsync();
                if (customer != null && customer.Count > 0)
                {
                    List<IBusinessResult> results = new List<IBusinessResult>();
                    foreach (var clinic in customer)
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
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(code);

                if (customer == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customer);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Customer customer)
        {
            try
            {
                //int result = await _currencyRepository.CreateAsync(currency);
                int result = await _unitOfWork.CustomerRepository.CreateAsync(customer);
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

        public async Task<IBusinessResult> Update(Customer customer)
        {
            try
            {
                //int result = await _currencyRepository.UpdateAsync(currency);
                int result = await _unitOfWork.CustomerRepository.UpdateAsync(customer);

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

                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(code);
                customer.IsActive = false;
                if (customer != null)
                {
                    //var result = await _currencyRepository.RemoveAsync(currency);
                    var result = await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, customer);
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

        public async Task<IBusinessResult> DeleteAllAsync()
        {
            try
            {
                var customers = await _unitOfWork.CustomerRepository.GetAllAsync();

                if (customers != null || customers.Any())
                {
                    foreach (var customer in customers)
                    {
                        await this.DeleteById(customer.CustomerId.ToString());
                    }
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, null);
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

        public async Task<IBusinessResult> Notification()
        {
            try
            {
                //var currency = await _currencyRepository.GetByIdAsync(code);
                var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
                foreach (var customer in customers)
                {
                    if ((bool)!customer.IsActive)
                    {
                        string subject = $"Thông báo từ hệ thống Booking Clinic: Xóa tài khoản thành công";
                        string bodyEmail = "Chúng tôi gửi đến bạn thông báo quan trọng về tài khoản của bạn trên hệ thống Booking Clinic." +
                            "Theo yêu cầu của bạn, chúng tôi đã xóa tài khoản của bạn khỏi hệ thống. " +
                            "Nếu bạn muốn tái kích hoạt tài khoản hoặc có bất kỳ câu hỏi nào, vui lòng liên hệ với chúng tôi qua email và số điện thoại bên dưới.";
                        await _commonService.SendEmailWithBodyAsync(customer.Email ?? "default_email@gmail.com", subject, customer.CustomerName, bodyEmail);
                    }
                }
                return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, customers);
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }
    }
}
