using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

public class EntityRepository<T>(TodoContext context) : IRepository<T> where T : class
{
    private readonly TodoContext context = context;

    public async Task<IEnumerable<T>> GetAsync(CancellationToken token = default) => await context.Set<T>().ToListAsync(token);

    public async Task<T> SingleAsync(int id, CancellationToken token = default) => await context.Set<T>().FindAsync([id], token);

    public async Task<T?> CreateAsync(T entity, CancellationToken token = default)
    {
        try
        {
            await context.AddAsync(entity, token);
            await context.SaveChangesAsync(token);
            return entity;
        }
        catch (DbUpdateException)
        {
            return null;
        }
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken token = default)
    {
        try
        {
            context.Update(entity);
            await context.SaveChangesAsync(token);
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(T entity, CancellationToken token = default)
    {
        try
        {
            context.Remove(entity);
            await context.SaveChangesAsync(token);
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }
}