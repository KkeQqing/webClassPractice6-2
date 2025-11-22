using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Yb.Bll.Sys;
using Yb.Model.Sys;
using Yb.Api.Controllers.Base;

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
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid input" });

            var user = _authBll.GetLoginUser(loginModel);
            if (user == null)
                return Unauthorized(new { message = "Invalid account or password" });

            var tokenModel = new TokenModel(user);
            tokenModel.Token = JwtHelper.IssueJWT(tokenModel);

            return Ok(new
            {
                success = true,
                data = tokenModel,
                message = "Login successful"
            });
        }
    }
}