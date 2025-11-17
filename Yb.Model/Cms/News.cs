using System;

namespace Yb.Model.Cms
{
    public class News
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string KeyWord { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Brief { get; set; } = string.Empty;
        public string Contents { get; set; } = string.Empty;
        public int Hit { get; set; }
        public int IsRec { get; set; }
        public int IsTop { get; set; }
        public int IsHead { get; set; }
        public string ModuleId { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Attach { get; set; } = string.Empty;
        public string CreateUserNM { get; set; } = string.Empty;
        public string CreateUserCD { get; set; } = string.Empty;
        public DateTime? CreateTime { get; set; }
        public string ModifyUserCD { get; set; } = string.Empty;
        public DateTime? ModifyTime { get; set; }
        public int CheckStatus { get; set; }
        public string CheckUserNM { get; set; } = string.Empty;
        public string CheckUserCD { get; set; } = string.Empty;
        public DateTime? CheckTime { get; set; }
        public string CheckMemo { get; set; } = string.Empty;
    }
}