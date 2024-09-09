using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemController(TodoItemService service, IMapper mapper) : ControllerBase
{
    private readonly TodoItemService todoItemService = service;
    private readonly IMapper mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetItemsAsync(CancellationToken token = default)
    {
        var items = await todoItemService.GetItemsAsync(token);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemAsync(int id, CancellationToken token = default)
    {
        var item = await todoItemService.GetItemAsync(id, token);
        return item == null ? NotFound(id) : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> PostItemAsync([FromBody] PostTodoItem model, CancellationToken token = default)
    {
        if (model is null || !ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (!await todoItemService.CreateItemAsync(model, token))
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutItemAsync(int id, [FromBody] PostTodoItem model, CancellationToken token)
    {
        if (model is null || !ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (!await todoItemService.UpdateItemAsync(id, model, token))
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        return Ok(model);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItemAsync(int id, CancellationToken token = default)
    {
        if (!await todoItemService.DeleteItemAsync(id, token))
        {
            return StatusCode((int)HttpStatusCode.NotFound);
        }

        return NoContent();
    }
}