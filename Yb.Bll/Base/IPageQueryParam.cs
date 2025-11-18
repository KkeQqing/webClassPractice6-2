namespace Yb.Bll.Base
{
    public interface IPageQueryParam
    {
        int pageIndex { get; set; }
        int pageSize { get; set; }
        DateTime? start { get; set; }
        DateTime? end { get; set; }
        string sort { get; set; }
        bool isAsc { get; set; }
    }
}