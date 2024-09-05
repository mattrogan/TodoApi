namespace TodoApi.Data;

public interface IRepository<T>
{
    Task<T> SingleAsync(int id, CancellationToken token = default);
    Task<IEnumerable<T>> GetAsync(CancellationToken token = default);
    Task<bool> CreateAsync(T entity, CancellationToken token = default);
    Task<bool> UpdateAsync(T entity, CancellationToken token = default);
    Task<bool> DeleteAsync(T entity, CancellationToken token = default);
}