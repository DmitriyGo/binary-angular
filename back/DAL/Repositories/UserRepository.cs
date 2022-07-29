using Core.Entities;
using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace DAL.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _db = applicationDbContext;
    }

    public Task<List<User>> GetAll()
    {
        return _db.Users.ToListAsync();
    }

    public Task<User> GetById(int id)
    {
        return _db.Users.FirstOrDefaultAsync(p => p.Id == id)!;
    }

    public async Task Create(User entity)
    {
        await _db.Users.AddAsync(entity);
        await SaveChanges();
    }

    public async Task Update(User entity)
    {
        _db.Users.Update(entity);
        await SaveChanges();
    }

    public async Task Delete(int id)
    {
        _db.Users.Remove((await _db.Users.FirstOrDefaultAsync(p => p.Id == id))!);
        await SaveChanges();
    }

    public async Task Delete(User entity)
    {
        _db.Users.Remove(entity);
        await SaveChanges();
    }

    public async Task SaveChanges()
    {
        await _db.SaveChangesAsync();
    }
}