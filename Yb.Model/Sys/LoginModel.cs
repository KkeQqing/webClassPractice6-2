namespace Yb.Model.Sys
{
    public class LoginModel
    {
        public string Account { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 用户输入的验证码
        /// </summary>
        public string CaptCode { get; set; } = string.Empty;

        /// <summary>
        /// 验证码对应的缓存 Key（由前端从 /captcha 接口获取）
        /// </summary>
        public string Key { get; set; } = string.Empty;
    }
}