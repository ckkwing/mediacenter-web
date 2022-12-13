using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Service.Data.Migrations.Base.BaseDb
{
    public partial class BaseDbMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "video",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "video");
        }
    }
}
