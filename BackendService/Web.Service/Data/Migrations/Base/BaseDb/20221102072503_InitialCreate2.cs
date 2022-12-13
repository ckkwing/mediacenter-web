using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Service.Data.Migrations.Base.BaseDb
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "size",
                table: "video",
                type: "bigint",
                nullable: false,
                comment: "In bytes(unit)",
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "size",
                table: "video",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "In bytes(unit)");
        }
    }
}
