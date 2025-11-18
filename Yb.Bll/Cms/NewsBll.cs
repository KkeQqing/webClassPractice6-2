using Yb.Bll.Base;
using Yb.Dal.Cms;
using Yb.Dal.Sys;
using Yb.Model.Cms;
using Yb.Model.Enums;
using Yb.Utility.StringUtility;

namespace Yb.Bll.Cms
{
    /// <summary>
    /// 新闻表业务逻辑
    /// </summary>
    public class NewsBll : BaseBll<News>
    {
        public NewsBll(NewsDal _dal) : base(_dal)
        {
        }

        #region 分页查询
        public ApiPagedList<News> GetList(NewsPQ param)
        {
            if (param == null)
                param = new NewsPQ();

            var query = base.Query();

            #region 查询条件
            if (!string.IsNullOrWhiteSpace(param.Title))
            {
                query = query.Where(o => o.Title.Contains(param.Title));
            }
            if (!string.IsNullOrWhiteSpace(param.Author))
            {
                query = query.Where(o => o.Author.Contains(param.Author));
            }
            if (!string.IsNullOrWhiteSpace(param.Source))
            {
                query = query.Where(o => o.Source.Contains(param.Source));
            }
            if (!string.IsNullOrWhiteSpace(param.KeyWord))
            {
                query = query.Where(o => o.KeyWord.Contains(param.KeyWord));
            }
            if (param.CheckStatus != default(EnumCheckStatus))
            {
                query = query.Where(o => o.CheckStatus == (int)param.CheckStatus);
            }
            #endregion

            #region 排序
            param.sort = string.IsNullOrWhiteSpace(param.sort) ? "CreateTime" : param.sort;

            switch (param.sort)
            {
                case "Title":
                    query = param.isAsc ? query.OrderBy(o => o.Title) : query.OrderByDescending(o => o.Title);
                    break;
                case "Author":
                    query = param.isAsc ? query.OrderBy(o => o.Author) : query.OrderByDescending(o => o.Author);
                    break;
                case "Source":
                    query = param.isAsc ? query.OrderBy(o => o.Source) : query.OrderByDescending(o => o.Source);
                    break;
                case "KeyWord":
                    query = param.isAsc ? query.OrderBy(o => o.KeyWord) : query.OrderByDescending(o => o.KeyWord);
                    break;
                case "CheckStatus":
                    query = param.isAsc ? query.OrderBy(o => o.CheckStatus) : query.OrderByDescending(o => o.CheckStatus);
                    break;
                default:
                    query = param.isAsc ? query.OrderBy(o => o.CreateTime) : query.OrderByDescending(o => o.CreateTime);
                    break;
            }
            #endregion

            var result = query.ToApiPagedList(param);
            return result;
        }
        #endregion

        #region 异步新增
        public async Task<News?> AddAsync(News model, string userCD, string userNM)
        {
            model.Id = GuidUtility.GetID();
            model.CreateUserCD = userCD;
            model.CreateUserNM = userNM;
            model.CreateTime = DateTime.Now;
            model.ModifyUserCD = "";
            model.ModifyTime = DateTime.Now;
            model.CheckStatus = (int)EnumCheckStatus.UnCommit;
            model.CheckUserNM = "";
            model.CheckUserCD = "";
            model.CheckTime = DateTime.Now;
            model.CheckMemo = "";

            var success = await base.AddAsync(model);
            return success ? model : null;
        }
        #endregion

        #region 异步更新
        public async Task<News?> UpdateAsync(News model, string userCD)
        {
            model.ModifyUserCD = userCD;
            model.ModifyTime = DateTime.Now;

            var success = await base.UpdateAsync(model);
            return success ? model : null;
        }
        #endregion

        #region 批量删除
        public async Task<bool> DeleteRangeAsync(string[] ids)
        {
            var list = Query(o => ids.Contains(o.Id)).ToList();
            return await base.DeleteRangeAsync(list);
        }
        #endregion
    }
}