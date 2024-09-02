using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

public class EntityRepository<T>(TodoContext context) : IRepository<T> where T : class
{
    private readonly TodoContext context = context;

    public async Task<IEnumerable<T>> GetAsync(CancellationToken token = default) => await context.Set<T>().ToListAsync(token);

    public async Task<T> SingleAsync(int id, CancellationToken token = default) => await context.Set<T>().FindAsync([id], token);
}