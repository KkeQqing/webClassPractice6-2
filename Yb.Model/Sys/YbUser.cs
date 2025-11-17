using System;

namespace Yb.Model.Sys
{
    public class YbUser
    {
        public string Id { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserNM { get; set; } = string.Empty;
        public string UserCD { get; set; } = string.Empty;
        public string Pinyin { get; set; } = string.Empty;
        public int Sex { get; set; }
        public string MobileNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DepartmentCD { get; set; } = string.Empty;
        public string DepartmentNM { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public int DataAuthority { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int IsActive { get; set; }
        public string RoleID { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
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