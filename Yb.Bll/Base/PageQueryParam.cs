namespace Yb.Bll.Base
{
    /// <summary>
    /// 分页查询参数
    /// </summary>
    public class PageQueryParam : IPageQueryParam
    {
        private int _p = 1;
        private int _s = 20;
        private string _sort = "Desc";
        private bool _asc = false;

        public int pageIndex
        {
            get { return _p; }
            set { _p = value; }
        }

        public int pageSize
        {
            get { return _s; }
            set { _s = value; }
        }

        public DateTime? start { get; set; }
        public DateTime? end { get; set; }

        public string sort
        {
            get { return _sort; }
            set { _sort = value; }
        }

        public bool isAsc
        {
            get { return _asc; }
            set { _asc = value; }
        }
    }
}