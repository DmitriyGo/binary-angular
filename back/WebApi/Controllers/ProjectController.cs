using BLL;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }
        // GET: api/Project
        [HttpGet]
        public async Task<List<Project>> Get()
        {
            return await _projectService.GetAll();
        }

        // GET: api/Project/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Project> Get(int id)
        {
            return await _projectService.GetById(id);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            if (project == null)
                return BadRequest("Bad input");
            
            await _projectService.Create(project);
            return Ok();
        }

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Project project)
        {
            await _projectService.Update(project);
        }
        

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _projectService.Delete(id);
        }
    }
}
