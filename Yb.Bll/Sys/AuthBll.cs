// Yb.Bll.Sys/AuthBll.cs
using Yb.Dal.Sys;
using Yb.Model.Sys;
using Yb.Utility.Security;

namespace Yb.Bll.Sys
{
    public class AuthBll
    {
        private readonly YbUserDal _userDal;
        private object _moduleDal;

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
        public List<MenuBar> GetMenuBars(int menuType)
        {
            
            

            var result = new List<MenuBar>();

            

            return result;
        }
        public async Task<bool> ChangePassword(string userId, ChangePwdModel model)
        {
            var user = _userDal.Find(userId); // 修正：使用实例字段 _userDal 调用 Find 方法
            if (user == null) return false;

            if (user.Password != SecurityUtility.BuildPassword(model.OldPassword))
                return false;

            user.Password = SecurityUtility.BuildPassword(model.NewPassword);
            return await _userDal.UpdateAsync(user); // 同理，使用 _userDal 调用 UpdateAsync
        }
    }
}