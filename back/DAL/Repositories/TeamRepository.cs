using Core.Entities;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace DAL.Repositories;

public class TeamRepository : IRepository<Team>
{
    private readonly ApplicationDbContext _db;
    public TeamRepository(ApplicationDbContext applicationDbContext)
    {
        _db = applicationDbContext;
    }

    public Task<List<Team>> GetAll()
    {
        return _db.Teams
            .Include(x => x.Projects)
            .Include(x => x.Users).ToListAsync();
    }

    public Task<Team> GetById(int id)
    {
        return _db.Teams
            .Include(x => x.Projects)
            .Include(x => x.Users)
            .FirstOrDefaultAsync(p => p.Id == id)!;
    }

    public async Task Create(Team entity)
    {
        await _db.Teams.AddAsync(entity);
        await SaveChanges();
    }

    public async Task Update(Team entity)
    {
        _db.Teams.Update(entity);
        await SaveChanges();
    }

    public async Task Delete(int id)
    {
        _db.Teams.Remove((await _db.Teams.FirstOrDefaultAsync(p => p.Id == id))!);
        await SaveChanges();
    }

    public async Task Delete(Team entity)
    {
        _db.Teams.Remove(entity);
        await SaveChanges();
    }

    public async Task SaveChanges()
    {
        await _db.SaveChangesAsync();
    }
}