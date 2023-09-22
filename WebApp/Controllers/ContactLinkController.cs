using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Interfaces;

namespace WebAppApi.Controllers
{
    [Authorize(Roles = nameof(Role.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactLinkController : ControllerBase
    {
        private readonly IBaseService<ContactLink> _linkService;

        public ContactLinkController(IBaseService<ContactLink> linkService)
        {
            _linkService = linkService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _linkService.GetAll();

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _linkService.Delete(id);

            return Ok(res);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ContactLink link)
        {
            _linkService.Create(link);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] ContactLink link)
        {
            var res = _linkService.Edit(id, link);

            return Ok(res);
        }

    }
}
