using System;

namespace Yb.Model.Cms
{
    public class News
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime PublishTime { get; set; }
        public int Status { get; set; } // 1:草稿, 2:发布
        public string CreateUserCD { get; set; }
        public DateTime CreateTime { get; set; }
        public string? ModifyUserCD { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}