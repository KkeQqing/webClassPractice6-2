using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Yb.Api.Controllers.Base;
using Yb.Bll.Sys;
using Yb.Model.Sys;
using Microsoft.Extensions.Caching.Memory;
using Yb.Utility.Security;

namespace Yb.Api.Controllers.Sys
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthBll _authBll;
        private readonly IMemoryCache _memoryCache;

        // 同时注入 AuthBll 和 IMemoryCache
        public AuthController(AuthBll authBll, IMemoryCache memoryCache)
        {
            _authBll = authBll;
            _memoryCache = memoryCache;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, msg = "Invalid input" });

            var user = _authBll.GetLoginUser(loginModel);
            if (user == null)
                return Unauthorized(new { success = false, msg = "Invalid account or password" });

            var tokenModel = new TokenModel(user);
            tokenModel.Token = JwtHelper.IssueJWT(tokenModel);

            return Ok(new ApiResult<TokenModel>(tokenModel, true)
            {
                Msg = "Login successful"
            });
        }

        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <param name="key">缓存 key，默认为 k1</param>
        /// <returns>图片流</returns>
        [HttpGet("captcha")]
        [AllowAnonymous]
        public FileContentResult GetCaptchaCode([FromQuery] string key = "k1")
        {
            var captchaHelper = new CaptchaHelper();
            string code = captchaHelper.CreateValidateCode(4); // 4位验证码
            byte[] imageData = captchaHelper.CreateValidateGraphic(code);

            // 将验证码存入内存缓存，10分钟后过期
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            _memoryCache.Set(key, code, cacheEntryOptions);

            return File(imageData, "image/jpeg");
        }
    }
}