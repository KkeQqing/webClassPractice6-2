using Yb.Bll.Base;
using Yb.Dal.Cms;
using Yb.Model.Cms;

namespace Yb.Bll.Cms
{
    public class NewsBll : BaseBll<News>
    {
        public NewsBll(NewsDal dal) : base(dal)
        {
        }
    }
}