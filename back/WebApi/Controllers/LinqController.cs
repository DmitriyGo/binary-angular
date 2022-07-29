
using BLL;
using Core.Entities;
using Core.Entities.LinqDTOs;
using Task = Core.Entities.Task;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LinqController : ControllerBase
    {
        private readonly LinqService _linqService;
        public LinqController(LinqService linqService)
        {
            _linqService = linqService;
        }
        
        [HttpGet(Name = "CountTasksInUser")]
        public async Task<List<TasksInUserDTO>> CountTasksInUser(int id)
        {
            return await _linqService.CountTasksInUser(id);
        }
        
        [HttpGet(Name = "GetUsersTasks")]
        public async Task<List<Task>> GetUsersTasks(int id)
        {
            return await _linqService.GetUsersTasks(id);
        }
        
        [HttpGet(Name = "GetRecentlyFinishedTasks")]
        public async Task<List<RecentlyFinishedTasksDTO>> GetRecentlyFinishedTasks(int id)
        {
            return await _linqService.GetRecentlyFinishedTasks(id);
        }
        
        [HttpGet(Name = "GetOldTeams")]
        public async Task<List<OldTeamsDTO>> GetOldTeams()
        {
            return await _linqService.GetOldTeams();
        }
        
        [HttpGet(Name = "GetListOfUsersWithTasks")]
        public async Task<List<UserWithTasksDTO>> GetListOfUsersWithTasks()
        {
            return await _linqService.GetListOfUsersWithTasks();
        }
        
        [HttpGet(Name = "GetUserStructure")]
        public async Task<UserStructureDTO>
            GetUserStructure(int userId)
        {
            return await _linqService.GetUserStructure(userId);
        }
        
        [HttpGet(Name = "GetProjectStructure")]
        public async Task<ProjectStructureDTO> GetProjectStructure(int projId)
        {
            return await _linqService.GetProjectStructure(projId);
        }
    }
}
