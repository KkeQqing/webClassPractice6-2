using Yb.Dal.Sys;
using Yb.Model.Sys;
using Yb.Utility.Security;

namespace Yb.Bll.Sys
{
    public class AuthBll
    {
        private readonly YbUserDal _userDal;

        public AuthBll(YbUserDal userDal)
        {
            _userDal = userDal;
        }

        public YbUser? GetLoginUser(LoginModel loginModel)
        {
            loginModel.Password = SecurityUtility.BuildPassword(loginModel.Password);
            return _userDal.Query(o => o.Account == loginModel.Account && o.Password == loginModel.Password).FirstOrDefault();
        }
    }
}