using Microsoft.AspNetCore.Mvc;
using Yb.Api.Controllers.Base;
using Yb.Bll.Cms;
using Yb.Model.Cms;

namespace Yb.Api.Controllers.Cms
{
    public class NewsController : BaseController
    {
        private readonly NewsBll _newsBll;

        public NewsController(NewsBll newsBll)
        {
            _newsBll = newsBll;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var newsList = _newsBll.Query().ToList();
            return Ok(newsList);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] News news)
        {
            news.Id = Guid.NewGuid().ToString();
            news.CreateTime = DateTime.Now;
            var result = await _newsBll.AddAsync(news);
            return Ok(new { success = result });
        }
    }
}