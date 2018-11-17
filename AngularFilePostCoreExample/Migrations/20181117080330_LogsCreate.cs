using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularFilePostCoreExample.Migrations
{
    public partial class LogsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LogType = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Checked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
