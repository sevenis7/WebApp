using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IService<Project> _projectService;

        public ProjectController(IService<Project> projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> All()
        {
            return Ok(await _projectService.All());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Get(int id)
        {
            var project = await _projectService.Get(id);

            if (project == null) return BadRequest();

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> Post([FromBody] Project project)
        {
            var response = await _projectService.Post(project);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> Put(int id, [FromBody] Project project)
        {
            var response = await _projectService.Edit(id, project);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> Delete(int id)
        {
            var response = await _projectService.Delete(id);

            if (response == null) return BadRequest();

            return Ok(response);
        }
    }
}
