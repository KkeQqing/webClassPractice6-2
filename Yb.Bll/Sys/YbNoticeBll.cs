using Yb.Bll.Base;
using Yb.Dal.Sys;
using Yb.Model.Base;
using Yb.Model.Enums;
using Yb.Model.Sys;
using Yb.Utility.StringUtility;

namespace Yb.Bll.Sys
{
    public class YbNoticeBll : BaseBll<YbNotice>
    {
        public YbNoticeBll(YbNoticeDal dal) : base(dal)
        {
        }

        public ApiPagedList<YbNotice> GetList(YbNoticePQ param)
        {
            if (param == null) param = new YbNoticePQ();

            var query = Query();

            if (!string.IsNullOrWhiteSpace(param.NoticeTypeCD))
                query = query.Where(o => o.NoticeTypeCD.Contains(param.NoticeTypeCD));
            if (!string.IsNullOrWhiteSpace(param.Title))
                query = query.Where(o => o.Title.Contains(param.Title));
            if (!string.IsNullOrWhiteSpace(param.Author))
                query = query.Where(o => o.Author.Contains(param.Author));
            if (!string.IsNullOrWhiteSpace(param.Source))
                query = query.Where(o => o.Source.Contains(param.Source));
            if (!string.IsNullOrWhiteSpace(param.KeyWord))
                query = query.Where(o => o.KeyWord.Contains(param.KeyWord));
            if (param.CheckStatus > 0)
                query = query.Where(o => o.CheckStatus == param.CheckStatus);

            param.sort = string.IsNullOrWhiteSpace(param.sort) ? "CreateTime" : param.sort;
            switch (param.sort)
            {
                case "Title": query = param.isAsc ? query.OrderBy(o => o.Title) : query.OrderByDescending(o => o.Title); break;
                case "Author": query = param.isAsc ? query.OrderBy(o => o.Author) : query.OrderByDescending(o => o.Author); break;
                case "Source": query = param.isAsc ? query.OrderBy(o => o.Source) : query.OrderByDescending(o => o.Source); break;
                case "KeyWord": query = param.isAsc ? query.OrderBy(o => o.KeyWord) : query.OrderByDescending(o => o.KeyWord); break;
                case "CheckStatus": query = param.isAsc ? query.OrderBy(o => o.CheckStatus) : query.OrderByDescending(o => o.CheckStatus); break;
                default: query = param.isAsc ? query.OrderBy(o => o.CreateTime) : query.OrderByDescending(o => o.CreateTime); break;
            }

            return query.ToApiPagedList(param.pageIndex, param.pageSize);
        }

        public async Task<YbNotice?> AddAsync(YbNotice model, TokenModel currentUser)
        {
            model.Id = GuidUtility.GetID();
            model.CreateUserCD = currentUser.UserCD;
            model.CreateUserNM = currentUser.UserNM;
            model.CreateTime = DateTime.Now;
            model.ModifyUserCD = currentUser.UserCD;
            model.ModifyTime = DateTime.Now;
            model.CheckStatus = (int)EnumCheckStatus.UnCommit;
            model.CheckUserNM = "";
            model.CheckUserCD = "";
            model.CheckTime = DateTime.Now;
            model.CheckMemo = "";

            var result = await base.AddAsync(model);
            return result ? model : null;
        }

        public async Task<YbNotice?> UpdateAsync(YbNotice model, string userCD)
        {
            model.ModifyUserCD = userCD;
            model.ModifyTime = DateTime.Now;
            var result = await base.UpdateAsync(model);
            return result ? model : null;
        }

        public async Task<bool> DeleteRangeAsync(string[] ids)
        {
            var list = Query(o => ids.Contains(o.Id)).ToList();
            return await base.DeleteRangeAsync(list);
        }

        public async Task<YbNotice> Check(CheckModel model, TokenModel currentUser)
        {
            var entity = Find(model.Id);
            if (entity == null) return null;

            entity.CheckUserCD = model.CheckUserCD ?? currentUser.UserCD;
            entity.CheckUserNM = model.CheckUserNM ?? currentUser.UserNM;
            entity.CheckTime = model.CheckTime ?? DateTime.Now;
            entity.CheckMemo = model.CheckMemo;

            if (model.CheckStatus > (int)EnumCheckStatus.CheckSuccess)
                model.CheckStatus = 0;

            entity.CheckStatus = model.CheckStatus;

            var success = await UpdateAsync(entity);
            return success ? entity : null;
        }

        public virtual Task<bool> Checks(CheckModelBatch model, TokenModel currentUser)
        {
            var entities = Query(o => model.Ids.Contains(o.Id)).ToList();
            foreach (var e in entities)
            {
                e.CheckUserCD = model.CheckUserCD ?? currentUser.UserCD;
                e.CheckUserNM = model.CheckUserNM ?? currentUser.UserNM;
                e.CheckTime = model.CheckTime ?? DateTime.Now;
                e.CheckMemo = model.CheckMemo;

                if (model.CheckStatus > (int)EnumCheckStatus.CheckSuccess)
                    model.CheckStatus = 0;

                e.CheckStatus = model.CheckStatus;
            }

            return base.UpdateRangeAsync(entities);
        }
    }
}