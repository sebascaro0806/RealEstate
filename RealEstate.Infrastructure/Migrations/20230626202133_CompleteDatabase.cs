using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompleteDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "BuildingProperties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BuildingPropertiesImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    BuildingPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingPropertiesImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingPropertiesImages_BuildingProperties_BuildingPropertyId",
                        column: x => x.BuildingPropertyId,
                        principalTable: "BuildingProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingPropertiesTraces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    BuildingPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingPropertiesTraces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingPropertiesTraces_BuildingProperties_BuildingPropertyId",
                        column: x => x.BuildingPropertyId,
                        principalTable: "BuildingProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildingProperties_OwnerId",
                table: "BuildingProperties",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingPropertiesImages_BuildingPropertyId",
                table: "BuildingPropertiesImages",
                column: "BuildingPropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuildingPropertiesTraces_BuildingPropertyId",
                table: "BuildingPropertiesTraces",
                column: "BuildingPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingProperties_Owners_OwnerId",
                table: "BuildingProperties",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingProperties_Owners_OwnerId",
                table: "BuildingProperties");

            migrationBuilder.DropTable(
                name: "BuildingPropertiesImages");

            migrationBuilder.DropTable(
                name: "BuildingPropertiesTraces");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_BuildingProperties_OwnerId",
                table: "BuildingProperties");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "BuildingProperties");
        }
    }
}
