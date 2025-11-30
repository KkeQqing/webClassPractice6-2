using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yb.Api.Controllers.Base;
using Yb.Bll.Sys;
using Yb.Model;
using Yb.Model.Base;
using Yb.Model.Sys;

namespace Yb.Api.Controllers.Sys
{
    [Route("api/Sys/[controller]/[action]")]
    [ApiController]
    public class YbNoticeController : BaseController
    {
        private readonly YbNoticeBll _ybNoticeBll;
        private readonly ILogger<YbNoticeController> _logger;

        public YbNoticeController(YbNoticeBll ybNoticeBll, ILogger<YbNoticeController> logger)
        {
            _ybNoticeBll = ybNoticeBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var entity = await _ybNoticeBll.FindAsync(id);
            if (entity == null)
                return Fail("未找到通知");
            return Ok(entity);
        }

        [HttpGet]
        public IActionResult GetList([FromQuery] YbNoticePQ pq)
        {
            var list = _ybNoticeBll.GetList(pq);
            return Ok(list); // ApiPagedList 已是数据，直接返回
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] YbNotice model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors, (key, error) => new ApiResultModelError
                {
                    
                    errors = new List<string> { string.IsNullOrEmpty(error.ErrorMessage) ? error.Exception?.Message ?? "无效输入" : error.ErrorMessage }
                }).ToList();
                return ValidationFail(errors);
            }

            var currentUser = GetCurrentUser(); // 👈 需要实现此方法
            var result = await _ybNoticeBll.AddAsync(model, currentUser);
            if (result != null)
                return Ok(result);
            else
                return Fail("新增失败");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] YbNotice model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors, (key, error) => new ApiResultModelError
                {
                    
                    errors = new List<string> { string.IsNullOrEmpty(error.ErrorMessage) ? error.Exception?.Message ?? "无效输入" : error.ErrorMessage }
                }).ToList();
                _logger.LogError("模型验证失败: {Errors}", string.Join(",", errors.SelectMany(e => e.errors)));
                return ValidationFail(errors);
            }

            var currentUser = GetCurrentUser();
            var result = await _ybNoticeBll.UpdateAsync(model, currentUser.UserCD);
            if (result != null)
                return Ok(result);
            else
                return Fail("更新失败");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Deletes([FromBody] string[] ids)
        {
            if (ids == null || ids.Length == 0)
                return Fail("ID不能为空");

            var success = await _ybNoticeBll.DeleteRangeAsync(ids);
            if (success)
                return Ok(true);
            else
                return Fail("删除失败");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Check([FromBody] CheckModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors, (key, error) => new ApiResultModelError
                {
                    
                    errors = new List<string> { string.IsNullOrEmpty(error.ErrorMessage) ? error.Exception?.Message ?? "无效输入" : error.ErrorMessage }
                }).ToList();
                _logger.LogError("模型验证失败: {Errors}", string.Join(",", errors.SelectMany(e => e.errors)));
                return ValidationFail(errors);
            }

            var currentUser = GetCurrentUser();
            var result = await _ybNoticeBll.Check(model, currentUser);
            if (result != null)
                return Ok(result);
            else
                return Fail("审核失败");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checks([FromBody] CheckModelBatch model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors, (key, error) => new ApiResultModelError
                {
                    
                    errors = new List<string> { string.IsNullOrEmpty(error.ErrorMessage) ? error.Exception?.Message ?? "无效输入" : error.ErrorMessage }
                }).ToList();
                _logger.LogError("模型验证失败: {Errors}", string.Join(",", errors.SelectMany(e => e.errors)));
                return ValidationFail(errors);
            }

            var currentUser = GetCurrentUser();
            var success = await _ybNoticeBll.Checks(model, currentUser);
            if (success)
                return Ok(true);
            else
                return Fail("批量审核失败");
        }

        // 👇 新增：从 HttpContext 获取 TokenModel（即 CurrentUser）
        private TokenModel GetCurrentUser()
        {
            var userCd = User.FindFirst("UserCD")?.Value;
            var userNm = User.FindFirst("UserNM")?.Value;
            // 或者从 JWT Claims 中获取，根据你的认证方式调整
            return new TokenModel
            {
                UserCD = userCd,
                UserNM = userNm
                // 其他字段按需补充
            };
        }
    }
}