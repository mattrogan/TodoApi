namespace TodoApi.Data;

public interface IRepository<T>
{
    Task<T> SingleAsync(int id, CancellationToken token = default);
    Task<IEnumerable<T>> GetAsync(CancellationToken token = default);
    // void AddAsync(T entity);
    // void UpdateAsync(T entity);
    // void DeleteAsync(T entity);
}