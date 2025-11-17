using Yb.Dal.Base;
using Yb.Model.Sys;

namespace Yb.Dal.Sys
{
    public class YbUserDal : BaseDal<YbUser>
    {
        public YbUserDal(SqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}