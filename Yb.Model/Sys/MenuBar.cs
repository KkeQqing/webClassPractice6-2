using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yb.Model.Sys
{
    public class MenuBar
    {
        public string? Id { get; set; }
        public string? PId { get; set; }
        public string? Title { get; set; }
        public string? SRef { get; set; }
        public string? Icon { get; set; }
        public string? Alert { get; set; }
        public List<MenuBar>? SubMenu { get; set; }

        public MenuBar() { }
        public MenuBar(string id, string pId, string title, string sRef, string icon, string alert = null)
        {
            Id = id;
            PId = pId;
            Title = title;
            SRef = sRef;
            Icon = icon;
            Alert = alert;
        }
    }
}
