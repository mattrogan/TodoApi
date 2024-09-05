using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController(EntityRepositoryFactory repositoryFactory, IMapper mapper) : ControllerBase
{
    private readonly IRepository<TodoItem> todoItemRepository = repositoryFactory.RepositoryFor<TodoItem>();
    private readonly IMapper mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetItemsAsync(CancellationToken token = default)
    {
        var items = await todoItemRepository.GetAsync(token);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemAsync(int id, CancellationToken token = default)
    {
        var item = await todoItemRepository.SingleAsync(id, token);
        IActionResult result = item is null
            ? NotFound(id)
            : Ok(item);
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> PostItemAsync([FromBody] PostTodoItem model, CancellationToken token = default)
    {
        if (model is null || !ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var item = mapper.Map<TodoItem>(model);

        if (!await todoItemRepository.CreateAsync(item, token))
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        return Created(nameof(GetItemAsync), item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutItemAsync(int id, [FromBody] PostTodoItem model, CancellationToken token)
    {
        if (model is null || !ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var item = await todoItemRepository.SingleAsync(id, token);
        if (item is null)
        {
            return NotFound(id);
        }

        mapper.Map(model, item);

        if (!await todoItemRepository.UpdateAsync(item, token))
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        return Ok(item);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItemAsync(int id, CancellationToken token = default)
    {
        var item = await todoItemRepository.SingleAsync(id, token);
        if (item is null)
            return NotFound(id);

        if (!await todoItemRepository.DeleteAsync(item, token))
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        return NoContent();
    }
}