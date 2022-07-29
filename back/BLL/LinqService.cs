using Core.Entities;
using Core.Entities.LinqDTOs;
using DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace BLL;

public class LinqService : IDisposable
{
    private  readonly HttpClient client = new();

    private IRepository<Project> _projectRepository;
    private IRepository<Team> _teamRepository;
    private IRepository<Core.Entities.Task> _taskRepository;
    private IRepository<User> _userRepository;

    public LinqService(IRepository<Project> projectRepository, IRepository<Team> teamRepository, IRepository<Core.Entities.Task> taskRepository, IRepository<User> userRepository)
    {
        _projectRepository = projectRepository;
        _teamRepository = teamRepository;
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }
    
    public void Dispose()
    {
        client.Dispose();
    }
    
    public async  Task<List<TasksInUserDTO>> CountTasksInUser(int id)
    {
        if (id < 0)
            throw new ArgumentException("Bad id input!");
        
        return (await _projectRepository.GetAll())
            .Where(t => t.AuthorId == id)
            .Select(p => new TasksInUserDTO()
            {
                Project = p,
                TasksCount = p.Tasks.Count()
            })
            .ToList();
    }

    public async Task<List<Core.Entities.Task>> GetUsersTasks(int id)
    {
        if (id < 0)
            throw new ArgumentException("Bad id input!");
        
        return (await _projectRepository.GetAll()).SelectMany(p => p.Tasks)
            .Where(t => t.PerformerId == id && t.Name.Length < 45)
            .ToList();
    }

    public async Task<List<RecentlyFinishedTasksDTO>> GetRecentlyFinishedTasks(int id)
    {
        if (id < 0)
            throw new ArgumentException("Bad id input!");
        return (await _projectRepository.GetAll()).SelectMany(p => p.Tasks)
            .Where(t => t.PerformerId == id && t.FinishedAt != null && t.FinishedAt.Value.Year == 2022)
            .Select(task => new RecentlyFinishedTasksDTO
            {

                Id = task.Id,
                Name = task.Name
            }).ToList();
    }

    public async Task<List<OldTeamsDTO>> GetOldTeams()
    {
        return  (await _teamRepository.GetAll()).Where(team => team.Users.All(user => GetAge(user.BirthDay) > 10))
            .Select(t => new OldTeamsDTO
            {
                Id = t.Id,
                TeamName = t.Name,
                Users = t.Users.OrderByDescending(u => u.BirthDay).ToArray()
            }).ToList();
    }

    public async Task<List<UserWithTasksDTO>> GetListOfUsersWithTasks()
    {
        return (await _taskRepository.GetAll()).GroupBy(t => t.PerformerId).Select(t => new UserWithTasksDTO
        {
            Name = _userRepository.GetById(t.Key.Value).Result.FirstName,
            Tasks = t.OrderByDescending(t => t.Name.Length).ToList()
        }).OrderBy(p => p.Name).ToList();
    }

    public async Task<UserStructureDTO> GetUserStructure(int id)
    {
        if (id < 0)
            throw new ArgumentException("Bad id input!");
        var neededTasks = (await _taskRepository.GetAll()).Where(t => t.PerformerId == id);

        var user = (await _userRepository.GetAll()).FirstOrDefault(u => u.Id == id);
        var lastProject = (await _projectRepository.GetAll())
            .Where(p => p.AuthorId == id)
            .MaxBy(p => p.CreatedAt);
        var tasksCount = neededTasks.Count();
        var notEndedTasks = neededTasks.Count(t => t.State != 2);
        var theLongestTask = neededTasks.MaxBy(t => t.FinishedAt - t.CreatedAt);

        return new UserStructureDTO
        {
            User = user,
            LastProject = lastProject,
            TasksCount = tasksCount,
            NotEndedTasks = notEndedTasks,
            TheLongestTask = theLongestTask
        };
    }

    public async Task<ProjectStructureDTO> GetProjectStructure(int id)
    {
        if (id < 0)
            throw new ArgumentException("Bad id input!");
        var project = (await _projectRepository.GetAll()).FirstOrDefault(p => p.Id == id);
        var taskWithLongestDescription = (await _taskRepository.GetAll()).Where(t => t.ProjectId == id).MaxBy(t => t.Description.Length);
        var taskWithShortestName = (await _taskRepository.GetAll()).Where(t => t.ProjectId == id).MinBy(t => t.Name.Length);
        var concreteUsersCount = 0;
        var taskCount = (await _taskRepository.GetAll()).Count(t => t.ProjectId == id);
        if ((await _projectRepository.GetAll()).First(p => p.Id == id).Description!.Length > 20 || taskCount < 3)
            concreteUsersCount = (await _taskRepository.GetAll())
                .Where(t => t.ProjectId == id)
                .Select(t => t.Performer)
                .Count();

        return new ProjectStructureDTO
        {
            Project = project,
            TaskWithLongestDescription = taskWithLongestDescription,
            TaskWithShortestName = taskWithShortestName,
            ConcreteUsersCount = concreteUsersCount,
        };
    }
    private static int GetAge(DateTime birthDate)
    {
        var now = DateTime.Today;
        return now.Year - birthDate.Year - 1 + 
               ((now.Month > birthDate.Month || now.Month == birthDate.Month && now.Day >= birthDate.Day) ? 1 : 0);
    }
    
}