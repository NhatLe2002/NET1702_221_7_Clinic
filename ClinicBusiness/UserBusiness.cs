using ClinicBusiness.Base;
using ClinicData.Models;
using ClinicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicCommon;

namespace ClinicBusiness
{
    public interface IUserBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Save(User user);
        Task<IBusinessResult> Update(User user);
        Task<IBusinessResult> DeleteById(string code);
    }
    public class UserBusinessClass : IUserBusiness
    {
        private readonly UnitOfWork _unitOfWork;


        public UserBusinessClass()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Task<IBusinessResult> DeleteById(string code)
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IBusinessResult> GetById(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<IBusinessResult> Save(User user)
        {
            try
            {
                int result = await _unitOfWork.UserRepository.CreateAsync(user);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG,user);
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

        public Task<IBusinessResult> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
