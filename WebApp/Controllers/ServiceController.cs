using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicerController : ControllerBase
    {
        private readonly IService<Service> _serviceService;

        public ServicerController(IService<Service> serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> All() => Ok(await  _serviceService.All());

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> Get(int id)
        {
            var service = await _serviceService.Get(id);

            return service == null ? BadRequest() : Ok(service);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Service>> Delete(int id)
        {
            var response = await _serviceService.Delete(id);

            return response == null ? BadRequest() : Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Service>> Post([FromBody] Service service)
        {
            var response = await _serviceService.Post(service);

            return response == null ? BadRequest() : Ok(service);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Service>> Edit(int id, [FromBody] Service service)
        {
            var response = await _serviceService.Edit(id, service);

            return response == null ? BadRequest() : Ok(response);
        }
    }
}
