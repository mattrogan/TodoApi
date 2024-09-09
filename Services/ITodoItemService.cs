using TodoApi.Models;

namespace TodoApi.Services;

/// <summary>
/// Interface for the service handling business logic for <see cref="TodoItem"/> instances. 
/// </summary>
public interface ITodoItemService
{
    Task<IEnumerable<TodoItem>> GetItemsAsync(CancellationToken token = default);
    Task<TodoItem> GetItemAsync(int id, CancellationToken token = default);
    Task<TodoItem?> CreateItemAsync(PostTodoItem model, CancellationToken token = default);
    Task<bool> UpdateItemAsync(int id, PostTodoItem model, CancellationToken token = default);
    Task<bool> DeleteItemAsync(int id, CancellationToken token = default);
}