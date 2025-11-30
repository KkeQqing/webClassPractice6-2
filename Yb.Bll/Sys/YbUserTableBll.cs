using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yb.Bll.Base;
using Yb.Model.Sys;
using Yb.Utility.StringUtility;
using Yb.Bll.Sys;

namespace Yb.Bll.Sys
{
    public class YbUserTableBll : BaseBll<YbUserTable>
    {
        public YbUserTableBll(YbUserTableDal dal) : base(dal) { }

        public ApiPagedList<YbUserTable> GetList(YbUserTablePQ param)
        {
            var query = Query().Where(x => x.UserCD == param.UserCD);
            return query.ToApiPagedList(param.pageIndex, param.pageSize);
        }

        public async Task<bool> AddRangeAsync(List<YbUserTable> models, string userCD)
        {
            foreach (var m in models)
            {
                m.Id = GuidUtility.GetID();
                m.UserCD = userCD;
            }
            return await base.AddRangeAsync(models);
        }
    }
}
