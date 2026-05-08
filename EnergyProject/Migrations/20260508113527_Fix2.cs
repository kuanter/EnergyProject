using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyProject.Migrations
{
    /// <inheritdoc />
    public partial class Fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_CardDatas_CardDataId",
                table: "Bills");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_CardDatas_CardDataId",
                table: "Bills",
                column: "CardDataId",
                principalTable: "CardDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_CardDatas_CardDataId",
                table: "Bills");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_CardDatas_CardDataId",
                table: "Bills",
                column: "CardDataId",
                principalTable: "CardDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
