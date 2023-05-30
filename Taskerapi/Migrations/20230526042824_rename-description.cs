using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskerapi.Migrations
{
    /// <inheritdoc />
    public partial class renamedescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Tasks",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tasks",
                newName: "Location");
        }
    }
}
