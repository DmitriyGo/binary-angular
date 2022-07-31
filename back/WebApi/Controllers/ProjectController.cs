using BLL;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly TaskService _taskService;
        public ProjectController(ProjectService projectService, TaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }
        // GET: api/Project
        [HttpGet]
        public async Task<List<Project>> Get()
        {
            return await _projectService.GetAll();
        }
        
        [HttpGet("{id}", Name = "GetProjectPage")]
        public async Task<List<Project>> GetPage(int id)
        {
            return (await _projectService.GetAll()).GetRange(id * 10, 10 );
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
            var collection = (await _projectService.GetById(id)).Tasks;
            
            if (collection != null)
                foreach (var task in collection)
                {
                    await _taskService.Delete(task.Id);
                }

            await _projectService.Delete(id);
        }
    }
}
