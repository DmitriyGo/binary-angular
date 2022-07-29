using Task = Core.Entities.Task;
using DAL.Interfaces;

namespace BLL;

public class TaskService
{
    private readonly IRepository<Task> _repository;
    public TaskService(IRepository<Task> repository)
    {
        _repository = repository;
    }
    public async Task<List<Task>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Task> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<List<Task>> GetNotEndedTasksInUser(int id)
    {
        if((await _repository.GetAll()).Any(t => t.PerformerId == id))
            return (await _repository.GetAll()).Where(t => t.PerformerId == id && t.State != 2).ToList();

        throw new ArgumentException();
    }

    public async System.Threading.Tasks.Task Create(Task entity)
    {
        if (entity.State > 3 || entity.State < 0)
            throw new ArgumentException("Invalid status");
        
        await _repository.Create(entity);
    }

    public async System.Threading.Tasks.Task Update(Task entity)
    {
        if (entity.State > 3 || entity.State < 0)
            throw new ArgumentException("Invalid status");
        
        await _repository.Update(entity);
    }

    public async System.Threading.Tasks.Task Delete(int id)
    {
        await _repository.Delete(id);
    }

    public async System.Threading.Tasks.Task Delete(Task entity)
    {
        await _repository.Delete(entity);
    }

    public async System.Threading.Tasks.Task SaveChanges()
    {
        await _repository.SaveChanges();
    }
}