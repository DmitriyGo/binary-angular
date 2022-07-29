using Core.Entities;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;


namespace DAL.Repositories;

public class TaskRepository : IRepository<Core.Entities.Task>
{
    private readonly ApplicationDbContext _db;
    public TaskRepository(ApplicationDbContext applicationDbContext)
    {
        _db = applicationDbContext;
    }
    public Task<List<Core.Entities.Task>> GetAll()
    {
        return _db.Tasks
            .Include(x => x.Performer)
            .Include(x => x.Project).ToListAsync();
    }

    public Task<Core.Entities.Task> GetById(int id)
    {
        return  _db.Tasks
            .Include(x => x.Performer)
            .Include(x => x.Project)
            .FirstOrDefaultAsync(p => p.Id == id)!;
    }

    public async Task Create(Core.Entities.Task entity)
    {
        await _db.Tasks.AddAsync(entity);
        await SaveChanges();
    }

    public async Task Update(Core.Entities.Task entity)
    {
        _db.Tasks.Update(entity);
        await SaveChanges();
    }

    public async Task Delete(int id)
    {
        _db.Tasks.Remove((await _db.Tasks.FirstOrDefaultAsync(p => p.Id == id))!);
        await SaveChanges();
    }

    public async Task Delete(Core.Entities.Task entity)
    {
        _db.Tasks.Remove(entity);
        await SaveChanges();
    }

    public async Task SaveChanges()
    {
        await _db.SaveChangesAsync();
    }
    
}