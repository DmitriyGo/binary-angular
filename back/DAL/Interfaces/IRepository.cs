using System.Linq.Expressions;
using Core.Entities;
using Core.Entities.Abstract;
using Task = System.Threading.Tasks.Task;

namespace DAL.Interfaces;

public interface IRepository<TEntity>
{
    Task<List<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task Create(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(int id);
    Task Delete(TEntity entity);
    Task SaveChanges();
}