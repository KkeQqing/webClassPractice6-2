namespace Yb.Model.Sys
{
    public class YbNotice
    {
        public string Id { get; set; }
        public string NoticeTypeCD { get; set; }
        public string NoticeTypeNM { get; set; }
        public string Title { get; set; }
        public string? Author { get; set; }
        public string? Source { get; set; }
        public string? KeyWord { get; set; }
        public string Contents { get; set; }
        public int Hit { get; set; }
        public string CreateUserNM { get; set; }
        public string CreateUserCD { get; set; }
        public DateTime CreateTime { get; set; }
        public string? ModifyUserCD { get; set; }
        public DateTime? ModifyTime { get; set; }
        public int CheckStatus { get; set; }
        public string? CheckUserNM { get; set; }
        public string? CheckUserCD { get; set; }
        public DateTime? CheckTime { get; set; }
        public string? CheckMemo { get; set; }
    }
}