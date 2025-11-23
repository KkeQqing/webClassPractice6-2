// Yb.Bll.Sys/AuthBll.cs
using Yb.Dal.Sys;
using Yb.Model.Sys;

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
            // 🔓 直接明文比对！不加密、不哈希、不验证
            return _userDal.Query(o =>
                o.Account == loginModel.Account &&
                o.Password == loginModel.Password &&
                o.IsActive == 1 // 建议保留状态检查
            ).FirstOrDefault();
        }
    }
}