using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly IRepository<TodoItem> todoItemRepository;

    public TodoController(EntityRepositoryFactory repositoryFactory)
    {
        todoItemRepository = repositoryFactory.RepositoryFor<TodoItem>();
    }

    [HttpGet]
    public async Task<IActionResult> GetItemsAsync(CancellationToken token = default)
    {
        var items = await todoItemRepository.GetAsync(token);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemsAsync(int id, CancellationToken token = default)
    {
        var item = await todoItemRepository.SingleAsync(id, token);
        IActionResult result = item is null
            ? NotFound(id)
            : Ok(item);
        return result;
    }
}