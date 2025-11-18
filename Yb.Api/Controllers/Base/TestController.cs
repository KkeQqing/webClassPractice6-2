using Microsoft.AspNetCore.Mvc;
using Yb.Api.Controllers.Base;

namespace Yb.Api.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet("success")]
        public IActionResult GetSuccess()
        {
            var data = new { Name = "张三", Age = 25 };
            return Ok(data); // 自动包装为 ApiResult
        }

        [HttpGet("fail")]
        public IActionResult GetFail()
        {
            return Fail("这是一个自定义错误");
        }

        [HttpGet("empty")]
        public IActionResult GetEmpty()
        {
            return Ok(); // 无数据成功响应
        }
    }
}