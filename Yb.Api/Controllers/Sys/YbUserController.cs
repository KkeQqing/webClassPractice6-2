using Microsoft.AspNetCore.Mvc;
using Yb.Api.Controllers.Base;
using Yb.Bll.Sys;
using Yb.Model.Sys;

namespace Yb.Api.Controllers.Sys
{
    public class YbUserController : BaseController
    {
        private readonly YbUserBll _ybUserBll;

        public YbUserController(YbUserBll ybUserBll)
        {
            _ybUserBll = ybUserBll;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _ybUserBll.Query().ToList();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] YbUser user)
        {
            user.Id = Guid.NewGuid().ToString();
            user.CreateTime = DateTime.Now;
            var result = await _ybUserBll.AddAsync(user);
            return Ok(new { success = result });
        }
    }
}