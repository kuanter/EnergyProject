using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyProject.Migrations
{
    /// <inheritdoc />
    public partial class Fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bills_CardDataId",
                table: "Bills");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CardDataId",
                table: "Bills",
                column: "CardDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bills_CardDataId",
                table: "Bills");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CardDataId",
                table: "Bills",
                column: "CardDataId",
                unique: true,
                filter: "[CardDataId] IS NOT NULL");
        }
    }
}
