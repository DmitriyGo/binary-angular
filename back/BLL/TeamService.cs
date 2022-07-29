using Core.Entities;
using DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace BLL;

public class TeamService
{
    private readonly IRepository<Team> _repository;
    private readonly IRepository<User> _userRepository;
    public TeamService(IRepository<Team> repository, IRepository<User> userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }
    public async Task AddUserToTeam(int teamId, int userId)
    {
        var user = _userRepository.GetById(userId);

        (await user).TeamId = teamId;

        await _userRepository.Update(await user);
    }
    public async Task<List<Team>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Team> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task Create(Team entity)
    {
        await _repository.Create(entity);
    }

    public async Task Update(Team entity)
    {
        await _repository.Update(entity);
    }

    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }

    public async Task Delete(Team entity)
    {
        await _repository.Delete(entity);
    }

    public async Task SaveChanges()
    {
        await _repository.SaveChanges();
    }
}