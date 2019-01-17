using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ncx.Exam.Api.Data.Migrations
{
    public partial class CreateBookSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    PublishDate = table.Column<DateTimeOffset>(nullable: false),
                    Rating = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
