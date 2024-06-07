using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenerateTicketService.Api.Migrations
{
    /// <inheritdoc />
    public partial class renameTikcetInfoColumnMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TicketInfo",
                newName: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "TicketInfo",
                newName: "Id");
        }
    }
}
