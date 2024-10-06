using System.Net;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Domain.Models;
using TodoApi.Services;
using TodoApi.Shared.DTOs;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemController(ITodoItemService service, IMapper mapper, IValidator<PostTodoItem> validator) : ControllerBase
{
    private readonly ITodoItemService todoItemService = service;
    private readonly IMapper mapper = mapper;
    private readonly IValidator<PostTodoItem> validator = validator;

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
        var validationResult = validator.Validate(model);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return ValidationProblem(ModelState);
        }

        var item = await todoItemService.CreateItemAsync(model, token);
        if (item is null)
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        return Created(nameof(this.GetItemAsync), new { id = item.Id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutItemAsync(int id, [FromBody] PostTodoItem model, CancellationToken token)
    {
        var validationResult = validator.Validate(model);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return ValidationProblem(ModelState);
        }

        if (!await todoItemService.UpdateItemAsync(id, model, token))
        {
            return NotFound();
        }

        return Ok(model);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItemAsync(int id, CancellationToken token = default)
    {
        if (!await todoItemService.DeleteItemAsync(id, token))
        {
            return NotFound();
        }

        return NoContent();
    }
}