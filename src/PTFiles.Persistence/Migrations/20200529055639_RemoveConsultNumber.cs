using Microsoft.EntityFrameworkCore.Migrations;

namespace PTFiles.Persistence.Migrations
{
    public partial class RemoveConsultNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Consultations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Consultations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
