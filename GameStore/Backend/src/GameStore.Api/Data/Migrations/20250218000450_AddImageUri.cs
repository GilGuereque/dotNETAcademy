using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Api.DataMigrations
{
    /// <inheritdoc />
    public partial class AddImageUri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUri",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUri",
                table: "Games");
        }
    }
}
