using Yb.Bll.Base;
using Yb.Dal.Sys;
using Yb.Model.Sys;

namespace Yb.Bll.Sys
{
    public class YbUserBll : BaseBll<YbUser>
    {
        public YbUserBll(YbUserDal dal) : base(dal)
        {
        }
    }
}