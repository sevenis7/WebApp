using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogArticleController : ControllerBase
    {
        private readonly IBlogArticleService _blogArticleService;

        public BlogArticleController(IBlogArticleService blogArticleService)
        {
            _blogArticleService = blogArticleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogArticle>>> All()
        {
            return Ok(await _blogArticleService.All());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogArticle>> Get(int id)
        {
            var blogArticle =  await _blogArticleService.Get(id);

            if (blogArticle == null) return BadRequest();

            return Ok(blogArticle);
        }

        [HttpPost]
        public async Task<ActionResult<BlogArticle>> Post([FromBody] BlogArticle article)
        {
            var response = await _blogArticleService.Post(article);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BlogArticle>> Delete(int id)
        {
            var response = await _blogArticleService.Delete(id);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BlogArticle>> Put(int id, [FromBody]  BlogArticle article)
        {
            var response = await _blogArticleService.Edit(id, article);

            if (response == null) return BadRequest();

            return Ok(response);
        }
    }
}
