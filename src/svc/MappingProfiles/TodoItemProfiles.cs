using AutoMapper;
using TodoApi.Domain.Models;
using TodoApi.Shared.DTOs;

namespace TodoApi.Services.MappingProfiles;

public class PostTodoItemToTodoItemProfile : Profile
{
    public PostTodoItemToTodoItemProfile()
    {
        CreateMap<PostTodoItem, TodoItem>();
    }
}
