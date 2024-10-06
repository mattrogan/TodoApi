using TodoApi.Domain.Models;

namespace TodoApi.Infrastructure.Data.SeedData;

public static class SeedData_TodoItems
{
    public static List<TodoItem> TodoItems => new()
    {
        new TodoItem
        {
            Id = 1,
            Name = "Foo",
            IsComplete = true
        },

        new TodoItem
        {
            Id = 2,
            Name = "Bar",
            IsComplete = false
        },

        new TodoItem
        {
            Id = 3,
            Name = "Baz",
            IsComplete = true
        }
    };
}