namespace Yb.Model.Sys
{
    public class TokenModel
    {
        public string Id { get; set; }
        public string Account { get; set; }
        public string UserNM { get; set; }
        public string UserCD { get; set; }
        public string? DepartmentCD { get; set; }
        public string? DepartmentNM { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string? Photo { get; set; }
        public int DataAuthority { get; set; }
        public DateTime TokenTime { get; set; } = DateTime.Now;
        public string? Token { get; set; }
        public string? TokenType { get; set; } = "Bearer";

        public Profile? profile;

        public TokenModel() { }

        public TokenModel(YbUser ybUser)
        {
            Id = ybUser.Id;
            Account = ybUser.Account;
            UserNM = ybUser.UserNM;
            UserCD = ybUser.UserCD;
            DepartmentCD = ybUser.DepartmentCD;
            DepartmentNM = ybUser.DepartmentNM;
            RoleID = ybUser.RoleID;
            RoleName = ybUser.RoleName;
            Photo = ybUser.Photo;
            DataAuthority = ybUser.DataAuthority;
            Profile profile = new Profile();
            profile.sid = ybUser.Id;
            profile.name = ybUser.UserNM;
        }
    }

    public class Profile
    {
        public string? sid { get; set; }
        public string? name { get; set; }
        public DateTime? auth_time { get; set; } = DateTime.Now;
        public DateTime? expires_at { get; set; } = DateTime.Now;
    }
}