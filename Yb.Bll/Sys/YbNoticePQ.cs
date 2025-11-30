using Yb.Bll.Base;
using Yb.Model.Base;

namespace Yb.Model.Sys
{
    public class YbNoticePQ : PageQueryParam
    {
        public string? NoticeTypeCD { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Source { get; set; }
        public string? KeyWord { get; set; }
        public int? CheckStatus { get; set; }
    }
}