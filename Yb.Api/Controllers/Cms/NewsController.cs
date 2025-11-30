using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yb.Api.Controllers.Base;
using Yb.Bll.Base;
using Yb.Bll.Cms;
using Yb.Model.Base;
using Yb.Model.Cms;

[Route("api/Cms/[controller]/[action]")]
[ApiController]
public class NewsController : BaseController
{
    private readonly NewsBll _newsBll;

    public NewsController(NewsBll newsBll)
    {
        _newsBll = newsBll;
    }

    [HttpGet]
    public ApiResult<ApiPagedList<News>> GetList([FromQuery] NewsPQ pq)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return new ApiResult<ApiPagedList<News>>(null, false) { Error = string.Join(",", errors) };
        }

        var list = _newsBll.GetList(pq);
        return new ApiResult<ApiPagedList<News>>(list, true);
    }

    [HttpPost]
    [Authorize]
    public async Task<ApiResult<News>> Post([FromBody] News model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return new ApiResult<News>(null, false) { Error = string.Join(",", errors) };
        }

        var result = await _newsBll.AddAsync(model, CurrentUser); // ❌ 类型不匹配！
        return result ?? new ApiResult<News>(null, false) { Error = "新增失败" };
    }
}