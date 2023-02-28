using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SelectionItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "CourseLevel",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SelectionItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectionItems");

            migrationBuilder.DropColumn(
                name: "CourseLevel",
                table: "Students");

            migrationBuilder.AddColumn<byte>(
                name: "Grade",
                table: "Students",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
