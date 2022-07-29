using Core.Entities;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DAL.Repositories;

public class ProjectRepository : IRepository<Project>
{
    private readonly ApplicationDbContext _db;

    public ProjectRepository(ApplicationDbContext applicationDbContext)
    {
        _db = applicationDbContext;
    }

    public Task<List<Project>> GetAll()
    {
        return _db.Projects
            .Include(x => x.Author)
            .Include(x => x.Team)
            .Include(x => x.Tasks).ToListAsync();
    }

    public Task<Project> GetById(int id)
    {
        return _db.Projects
            .Include(x => x.Author)
            .Include(x => x.Team)
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id)!;
    }

    public async Task Create(Project entity)
    {
        _db.Projects.Add(entity);
        await SaveChanges();
    }

    public async Task Update(Project entity)
    {
        _db.Projects.Update(entity);
        await SaveChanges();
    }

    public async Task Delete(int id)
    {
        _db.Projects.Remove((await _db.Projects.FirstOrDefaultAsync(p => p.Id == id))!);
        await SaveChanges();
    }

    public async Task Delete(Project entity)
    {
        _db.Projects.Remove(entity);
        await SaveChanges();
    }

    public async Task SaveChanges()
    {
        await _db.SaveChangesAsync();
    }
}