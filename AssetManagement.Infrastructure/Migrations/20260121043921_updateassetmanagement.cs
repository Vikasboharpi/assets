using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AssetManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateassetmanagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assetmanagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "text", nullable: false),
                    AssetName = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    RAM = table.Column<string>(type: "text", nullable: false),
                    Storage = table.Column<string>(type: "text", nullable: false),
                    Processor = table.Column<string>(type: "text", nullable: false),
                    StorageSize = table.Column<int>(type: "integer", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assetmanagement", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assetmanagement");
        }
    }
}
