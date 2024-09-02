using AutoMapper;
using TodoApi.Models;

namespace TodoApi.MappingProfiles;

public class PostTodoItemToTodoItemProfile : Profile
{
    public PostTodoItemToTodoItemProfile()
    {
        CreateMap<PostTodoItem, TodoItem>();
    }
}
