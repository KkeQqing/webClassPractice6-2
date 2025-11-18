using Microsoft.AspNetCore.Mvc;
using Yb.Api.Controllers.Base;

namespace Yb.Api.Controllers
{
    [ApiController] // 👈 必须加上！否则 Swagger 不识别
    [Route("api/[controller]")]
    public class TestController : BaseController
    {
        [HttpGet("success")]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status200OK)]
        public IActionResult GetSuccess()
        {
            var data = new { Name = "张三", Age = 25 };
            return Ok(data);
        }

        [HttpGet("fail")]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status400BadRequest)]
        public IActionResult GetFail()
        {
            return Fail("这是一个自定义错误");
        }

        [HttpGet("empty")]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status200OK)]
        public IActionResult GetEmpty()
        {
            return Ok();
        }
    }
}