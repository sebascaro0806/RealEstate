using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedListImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BuildingPropertiesImages_BuildingPropertyId",
                table: "BuildingPropertiesImages");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "BuildingPropertiesImages");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "BuildingPropertiesImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "BuildingPropertiesImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingPropertiesImages_BuildingPropertyId",
                table: "BuildingPropertiesImages",
                column: "BuildingPropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BuildingPropertiesImages_BuildingPropertyId",
                table: "BuildingPropertiesImages");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "BuildingPropertiesImages");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "BuildingPropertiesImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "BuildingPropertiesImages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_BuildingPropertiesImages_BuildingPropertyId",
                table: "BuildingPropertiesImages",
                column: "BuildingPropertyId",
                unique: true);
        }
    }
}
