using Microsoft.AspNetCore.Mvc;
using Yb.Api.Controllers.Base;

namespace Yb.Api.Controllers.Base
{
    /// <summary>
    /// 控制器基类，用于统一返回格式
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// 成功返回
        /// </summary>
        protected IActionResult Ok<T>(T data)
        {
            var result = new ApiResult<T>(data, true) { Code = 200 };
            return base.Ok(result);
        }

        /// <summary>
        /// 成功返回（无数据）
        /// </summary>
        protected IActionResult Ok()
        {
            var result = new ApiResult<object>(null, true) { Code = 200, Msg = "操作成功" };
            return base.Ok(result);
        }

        /// <summary>
        /// 失败返回（带错误信息）
        /// </summary>
        protected IActionResult Fail(string errorMsg, int code = 500)
        {
            var result = new ApiResult<object>(default(object), false)
            {
                Code = code,
                Error = errorMsg
            };
            return BadRequest(result);
        }

        /// <summary>
        /// 模型验证失败返回
        /// </summary>
        protected IActionResult ValidationFail(List<ApiResultModelError> errors)
        {
            var result = new ApiResult<object>(default(object), false)
            {
                Code = 400,
                ModelErrors = errors,
                Error = string.Join(",", errors.SelectMany(e => e.errors))
            };
            return BadRequest(result);
        }
    }
}