using Microsoft.EntityFrameworkCore;
using Yb.Dal.Base;
using Yb.Model.Sys;

namespace Yb.Dal.Sys
{
    public class YbNoticeDal : BaseDal<YbNotice>
    {
        public YbNoticeDal(SqlDbContext context) : base(context)
        {
        }
    }
}