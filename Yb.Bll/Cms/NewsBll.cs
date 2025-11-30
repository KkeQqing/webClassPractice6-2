using Yb.Bll.Base;
using Yb.Bll.Cms;
using Yb.Model.Sys;
using Yb.Utility.StringUtility;
using Yb.Model.Cms;
using Yb.Dal.Cms;

namespace Yb.Bll.Cms
{

    public class NewsBll : BaseBll<News>
    {
        public NewsBll(NewsDal dal) : base(dal) { }

        public ApiPagedList<News> GetList(NewsPQ param)
        {
            var query = Query();
            if (!string.IsNullOrWhiteSpace(param.Title))
                query = query.Where(x => x.Title.Contains(param.Title));


            return query.ToApiPagedList(param.pageIndex, param.pageSize);
        }

        public async Task<News?> AddAsync(News model, TokenModel user)
        {
            model.Id = GuidUtility.GetID();
            model.CreateUserCD = user.UserCD;
            model.CreateTime = DateTime.Now;
            model.Status = 1;
            var result = await base.AddAsync(model);
            return result ? model : null;
        }

        public async Task<News?> UpdateAsync(News model, string userCD)
        {
            model.ModifyUserCD = userCD;
            model.ModifyTime = DateTime.Now;
            var result = await base.UpdateAsync(model);
            return result ? model : null;
        }
    }
}