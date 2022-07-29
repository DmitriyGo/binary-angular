using Core.Entities;
using DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace BLL;

public class ProjectService
{
    private readonly IRepository<Project> _repository;
    public ProjectService(IRepository<Project> repository)
    {
        _repository = repository;
    }
    public async Task<List<Project>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Project> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task Create(Project entity, string createdBy = null)
    {
        await _repository.Create(entity);
    }

    public async Task Update(Project entity, string modifiedBy = null)
    {
        await _repository.Update(entity);
    }

    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }

    public async Task Delete(Project entity)
    {
        await _repository.Delete(entity);
    }

    public async Task SaveChanges()
    {
        await _repository.SaveChanges();
    }
}