using BLL;
using Task = Core.Entities.Task;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }
        // GET: api/Task
        [HttpGet]
        public async Task<List<Task>> Get()
        {
            return await _taskService.GetAll();
        }
        
        [HttpGet("{id}", Name = "GetTaskPage")]
        public async Task<List<Task>> GetPage(int id)
        {
            return (await _taskService.GetAll()).GetRange(id * 10, 10 );
        }

        // GET: api/Task/5
        [HttpGet("{id}", Name = "GetTask")]
        public async Task<Task>Get(int id)
        {
            return await _taskService.GetById(id);
        }

        // POST: api/Task
        [HttpPost]
        public async System.Threading.Tasks.Task Post([FromBody] Task Task)
        {
            await _taskService.Create(Task);
        }

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task  Put(int id, [FromBody] Task Task)
        {
            await _taskService.Update(Task);
        }
        
        // GET: api/Task/5
        [HttpGet("{id}", Name = "GetNotEndedTasksInUser")]
        public async Task<List<Task>> GetNotEndedTasksInUser(int id)
        {
            try
            {
                return await _taskService.GetNotEndedTasksInUser(id);
            }
            catch (ArgumentException e)
            {
                return new List<Task>();
            }
            
        }
        

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_taskService.GetById(id) == null)
                return BadRequest("Bad input");
            
            await _taskService.Delete(id);
            return Ok();
        }
    }
}
