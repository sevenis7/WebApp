using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;
using System.Security.Claims;
using WebAppApi.Requests;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetAll()
        {
            var res = _requestService.All();

            return Ok(res);
        }

        [Authorize(Roles = nameof(Role.User))]
        [HttpPost("post")]
        public IActionResult Post([FromBody] PostRequest request)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out Guid userId)) return BadRequest();

            var requestDto = new Request
            {
                Text = request.Text,
            };

            _requestService.Post(requestDto, userId);

            return Ok();
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPut("{id}")]
        public IActionResult ChangeRequestStatus(int id, [FromBody] StatusRequest request)
        {
            var status = Enum.Parse<RequestStatus>(request.Status);

            var response = _requestService.ChangeStatus(id, status);

            return Ok(response);
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpGet("{requestStatus}")]
        public IActionResult GetCategoryRequest(RequestStatus requestStatus)
        {
            var response = _requestService.GetByStatus(requestStatus);

            return Ok(response);
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpGet("interval")]
        public IActionResult GetByTime([FromQuery] IntervalRequest interval)
        {
            var response = _requestService.GetByTime(interval.From, interval.To);

            return Ok(response);
        }
    }
}
