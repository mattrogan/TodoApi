using Microsoft.EntityFrameworkCore.Migrations;
using TodoApi.Infrastructure.Data.SeedData;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTodoItemSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var item in SeedData_TodoItems.TodoItems)
            {
                migrationBuilder.InsertData(
                    table: "TodoItems",
                    columns: ["Id", "Name", "IsComplete"],
                    values: [item.Id, item.Name, item.IsComplete]);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var item in SeedData_TodoItems.TodoItems)
            {
                migrationBuilder.DeleteData(
                    table: "TodoItems",
                    keyColumn: "Id",
                    keyValue: item.Id);
            }
        }
    }
}
