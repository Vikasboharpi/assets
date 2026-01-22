using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateAssetsmanagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentType",
                table: "Assetmanagement",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentType",
                table: "Assetmanagement");
        }
    }
}
