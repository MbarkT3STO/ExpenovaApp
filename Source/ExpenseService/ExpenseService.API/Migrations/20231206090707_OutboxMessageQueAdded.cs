using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseService.API.Migrations
{
    /// <inheritdoc />
    public partial class OutboxMessageQueAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QueueName",
                table: "OutboxMessages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QueueName",
                table: "OutboxMessages");
        }
    }
}
