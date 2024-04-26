using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shopsport.Migrations
{
    public partial class DbContext_shop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsPay",
                schema: "order",
                table: "Order",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPay",
                schema: "order",
                table: "Order");
        }
    }
}
