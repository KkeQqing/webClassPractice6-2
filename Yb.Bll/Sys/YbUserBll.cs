using Yb.Bll.Base;
using Yb.Dal.Sys;
using Yb.Model.Enums;
using Yb.Model.Sys;
using Yb.Utility.StringUtility;

namespace Yb.Bll.Sys
{
    /// <summary>
    /// 用户业务逻辑
    /// </summary>
    public class YbUserBll : BaseBll<YbUser>
    {
        public YbUserBll(YbUserDal _dal) : base(_dal)
        {
        }

        #region 分页查询
        public ApiPagedList<YbUser> GetList(YbUserPQ param)
        {
            if (param == null)
                param = new YbUserPQ();

            var query = base.Query();

            #region 查询条件
            if (!string.IsNullOrWhiteSpace(param.Account))
            {
                query = query.Where(o => o.Account.Contains(param.Account));
            }
            if (!string.IsNullOrWhiteSpace(param.UserNM))
            {
                query = query.Where(o => o.UserNM.Contains(param.UserNM));
            }
            if (!string.IsNullOrWhiteSpace(param.UserCD))
            {
                query = query.Where(o => o.UserCD.Contains(param.UserCD));
            }
            if (!string.IsNullOrWhiteSpace(param.DepartmentCD))
            {
                query = query.Where(o => o.DepartmentCD.Contains(param.DepartmentCD));
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
                case "Account":
                    query = param.isAsc ? query.OrderBy(o => o.Account) : query.OrderByDescending(o => o.Account);
                    break;
                case "UserNM":
                    query = param.isAsc ? query.OrderBy(o => o.UserNM) : query.OrderByDescending(o => o.UserNM);
                    break;
                case "UserCD":
                    query = param.isAsc ? query.OrderBy(o => o.UserCD) : query.OrderByDescending(o => o.UserCD);
                    break;
                case "DepartmentCD":
                    query = param.isAsc ? query.OrderBy(o => o.DepartmentCD) : query.OrderByDescending(o => o.DepartmentCD);
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
        public async Task<YbUser?> AddAsync(YbUser model, string userCD, string userNM)
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
        public async Task<YbUser?> UpdateAsync(YbUser model, string userCD)
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