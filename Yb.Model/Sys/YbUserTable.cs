using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yb.Model.Sys
{
    public class YbUserTable
    {
        public string Id { get; set; }
        public string UserCD { get; set; }
        public string TableName { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
    }
}
