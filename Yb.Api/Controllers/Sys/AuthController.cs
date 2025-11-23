using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Yb.Api.Controllers.Base;
using Yb.Bll.Sys;
using Yb.Model.Sys;

namespace Yb.Api.Controllers.Sys
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthBll _authBll;

        public AuthController(AuthBll authBll)
        {
            _authBll = authBll;
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
    }
}