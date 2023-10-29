using ServiceLayer.DTO;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using System.Security.Claims;
using WebAppApi.Requests;
using ServiceLayer.Requests;
using ServiceLayer.RequestServices;

namespace WebAppApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        //[Authorize(Roles = nameof(Role.Admin))]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Request>>> All()
        {
            return Ok(await _requestService.All());
        }

        //[Authorize(Roles = nameof(Role.User))]
        [HttpPost()]
        public async Task<ActionResult<Request>> Post([FromBody] PostRequest request)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out Guid userId)) return Unauthorized();

            var requestDto = new Request
            {
                Text = request.Text!,
            };

            var response = await _requestService.Post(requestDto, userId);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        //[Authorize(Roles = nameof(Role.Admin))]
        [HttpPut("{id}")]
        public async Task<ActionResult<Request>> Put(int id, [FromBody] Request request)
        {
            var response = await _requestService.Edit(id, request);

            if (response == null) return BadRequest();
                
            return Ok(response);
        }

        //[Authorize(Roles = nameof(Role.Admin))]
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> Get(int id)
        {
            var response = await _requestService.Get(id);

            if (response == null) return BadRequest();

            return Ok(response);
        }
    }

}
