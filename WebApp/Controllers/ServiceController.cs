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
    public class ServiceController : ControllerBase
    {
        private readonly IBaseService<Service> _serviceService;

        public ServiceController(IBaseService<Service> serviceService)
        {
            _serviceService = serviceService;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _serviceService.GetAll();

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _serviceService.Delete(id);

            return Ok(res);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Service service)
        {
            _serviceService.Create(service);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Service service)
        {
            var res = _serviceService.Edit(id, service);

            return Ok(res);
        }
    }
}
