using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.MappingProfiles;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TodoContext>(opts =>
{
    var dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    var path = Path.Combine(dir, "TodoApi", "todoapp.db");
    Directory.CreateDirectory(Path.GetDirectoryName(path));

    opts.UseSqlite($"Data Source={path}");
});

builder.Services.AddScoped<TodoItemService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));

builder.Services.AddAutoMapper(typeof(PostTodoItemToTodoItemProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
