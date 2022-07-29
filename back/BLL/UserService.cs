using Core.Entities;
using DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace BLL;

public class UserService
{
    private readonly IRepository<User> _repository;
    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }
    public async Task<List<User>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<User> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task Create(User entity)
    {
        await _repository.Create(entity);
    }

    public async Task Update(User entity)
    {
        await _repository.Update(entity);
    }

    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }

    public async Task Delete(User entity)
    {
        await _repository.Delete(entity);
    }

    public async Task SaveChanges()
    {
        await _repository.SaveChanges();
    }
}