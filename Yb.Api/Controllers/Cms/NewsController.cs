using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Yb.Bll.Base;
using Yb.Bll.Cms;
using Yb.Model.Cms;

namespace Yb.Api.Controllers.Cms
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // ✅ 整个控制器受保护
    public class NewsController : ControllerBase
    {
        private readonly NewsBll _newsBll;

        public NewsController(NewsBll newsBll)
        {
            _newsBll = newsBll;
        }

        /// <summary>
        /// 分页查询新闻列表
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] NewsPQ param)
        {
            var result = await Task.FromResult(_newsBll.GetList(param));
            return Ok(result);
        }

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] News model)
        {
            var userCD = "admin"; // 模拟登录用户
            var userNM = "管理员";

            var result = await _newsBll.AddAsync(model, userCD, userNM);
            if (result != null)
            {
                return CreatedAtAction(nameof(GetList), new { id = result.Id }, result);
            }
            return BadRequest("添加失败");
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] News model)
        {
            if (model.Id != id)
            {
                return BadRequest("ID 不匹配");
            }

            var userCD = "admin";
            var result = await _newsBll.UpdateAsync(model, userCD);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("更新失败");
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] string[] ids)
        {
            var success = await _newsBll.DeleteRangeAsync(ids);
            if (success)
            {
                return Ok(new { message = "删除成功" });
            }
            return BadRequest("删除失败");
        }
    }
}