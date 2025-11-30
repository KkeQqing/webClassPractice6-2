namespace Yb.Model.Base
{
    public class CheckModel
    {
        public string Id { get; set; }
        public int CheckStatus { get; set; }
        public string? CheckUserCD { get; set; }
        public string? CheckUserNM { get; set; }
        public DateTime? CheckTime { get; set; }
        public string? CheckMemo { get; set; }
    }

    public class CheckModelBatch
    {
        public List<string> Ids { get; set; }
        public int CheckStatus { get; set; }
        public string? CheckUserCD { get; set; }
        public string? CheckUserNM { get; set; }
        public DateTime? CheckTime { get; set; }
        public string? CheckMemo { get; set; }
    }
}