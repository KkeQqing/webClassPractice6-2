using Yb.Dal.Base;
using Yb.Model.Cms;

namespace Yb.Dal.Cms
{
    public class NewsDal : BaseDal<News>
    {
        public NewsDal(SqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}