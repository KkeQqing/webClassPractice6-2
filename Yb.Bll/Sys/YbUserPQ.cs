using Yb.Bll.Base;
using Yb.Model.Enums;

namespace Yb.Bll.Sys
{
    /// <summary>
    /// 用户分页查询参数
    /// </summary>
    public class YbUserPQ : PageQueryParam
    {
        public string Account { get; set; }
        public string UserNM { get; set; }
        public string UserCD { get; set; }
        public string DepartmentCD { get; set; }
        public EnumCheckStatus CheckStatus { get; set; }
    }
}