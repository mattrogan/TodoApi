using AutoMapper;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Services;

/// <summary>
/// Class that handles business logic and rules for todo items
/// </summary>
public class TodoItemService(IRepository<TodoItem> repository, IMapper mapper) : ITodoItemService
{
    private readonly IRepository<TodoItem> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> CreateItemAsync(PostTodoItem model, CancellationToken token = default)
    {
        var item = _mapper.Map<TodoItem>(model);
        return await _repository.CreateAsync(item, token);
    }

    public async Task<bool> DeleteItemAsync(int id, CancellationToken token = default)
    {
        var item = await _repository.SingleAsync(id, token);
        if (item == null)
        {
            return false;
        }

        return await _repository.DeleteAsync(item, token);
    }

    public async Task<TodoItem> GetItemAsync(int id, CancellationToken token = default)
        => await _repository.SingleAsync(id, token);

    public async Task<IEnumerable<TodoItem>> GetItemsAsync(CancellationToken token = default)
        => await _repository.GetAsync(token);

    public async Task<bool> UpdateItemAsync(int id, PostTodoItem model, CancellationToken token = default)
    {
        var item = await _repository.SingleAsync(id, token);
        if (item == null)
        {
            return false;
        }

        _mapper.Map(model, item);
        return await _repository.UpdateAsync(item, token);
    }
}