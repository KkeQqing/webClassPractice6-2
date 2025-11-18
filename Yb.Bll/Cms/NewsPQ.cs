using Yb.Bll.Base;
using Yb.Model.Enums;

namespace Yb.Bll.Cms
{
    /// <summary>
    /// 新闻分页查询参数
    /// </summary>
    public class NewsPQ : PageQueryParam
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public string KeyWord { get; set; }
        public EnumCheckStatus CheckStatus { get; set; }
    }
}