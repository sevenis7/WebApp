using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using WebAppApi.Requests;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public MainContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet("title")]
        public IActionResult Title()
        {
            return Ok(_contentService.Get());
        }

        [HttpPost("edit")]
        [Authorize(Roles = nameof(Role.Admin))]
        public IActionResult ChangeTitle([FromBody] ChangeTitleRequest title)
        {
            _contentService.ChangeTitle(title.Title);
            return Ok(_contentService.Get());
        }

    }
}
