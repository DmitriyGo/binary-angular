using BLL;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;
        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }
        // GET: api/Team
        [HttpGet]
        public async Task<List<Team>> Get()
        {
            return await _teamService.GetAll();
        }

        // GET: api/Team/5
        [HttpGet("{id}", Name = "GetTeam")]
        public async Task<Team> Get(int id)
        {
            return await _teamService.GetById(id);
        }

        // POST: api/Team
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Team team)
        {
            if (team == null)
                return BadRequest("Bad input");
            
            await _teamService.Create(team);
            return Ok();
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task  Put(int id, [FromBody] Team team)
        {
            await _teamService.Update(team);
        }
        

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task  Delete(int id)
        {
           await _teamService.Delete(id);
        }
    }
}
