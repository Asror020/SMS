using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Confirmation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUserConfirmed",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUserConfirmed",
                table: "Users");
        }
    }
}
