using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T>(StoreContext dbContext) : IGenericRepository<T> where T : BaseEntity
{
    public async Task<T?> GetByIdAsync(int id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>?> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public void Add(T entity)
    {
        dbContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        dbContext.Set<T>().Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(T entity)
    {
        dbContext.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await dbContext.SaveChangesAsync() > 0;
    }

    public bool Exists(int id)
    {
        return dbContext.Set<T>().Any(x => x.Id == id);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(dbContext.Set<T>().AsQueryable(), spec);
    }
}