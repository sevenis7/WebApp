using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace WebAppApi.Controllers
{
    [Authorize(Roles = nameof(Role.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IBaseService<Project> _projectService;

        public ProjectController(IBaseService<Project> projectService)
        {
            _projectService = projectService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _projectService.GetAll();

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _projectService.Delete(id);

            return Ok(res);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Project project)
        {
            _projectService.Create(project);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Project project)
        {
            var res = _projectService.Edit(id, project);

            return Ok(res);
        }
    }
}
