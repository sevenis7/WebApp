using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Interfaces;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IBaseService<Article> _articleService;

        public ArticleController(IBaseService<Article> articleService)
        {
            _articleService = articleService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _articleService.GetAll();

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _articleService.Delete(id);

            return Ok(res);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Article article)
        {
            _articleService.Create(article);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Article article)
        {
            var res = _articleService.Edit(id, article);

            return Ok(res);
        }
    }
}
