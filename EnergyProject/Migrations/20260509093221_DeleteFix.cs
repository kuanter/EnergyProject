using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyProject.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_MeterReadings_Meters_MeterId",
                table: "MeterReadings");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccounts_AspNetUsers_UserId",
                table: "PaymentAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses",
                column: "PaymentAccountId",
                principalTable: "PaymentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeterReadings_Meters_MeterId",
                table: "MeterReadings",
                column: "MeterId",
                principalTable: "Meters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAccounts_AspNetUsers_UserId",
                table: "PaymentAccounts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_MeterReadings_Meters_MeterId",
                table: "MeterReadings");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccounts_AspNetUsers_UserId",
                table: "PaymentAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses",
                column: "PaymentAccountId",
                principalTable: "PaymentAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeterReadings_Meters_MeterId",
                table: "MeterReadings",
                column: "MeterId",
                principalTable: "Meters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAccounts_AspNetUsers_UserId",
                table: "PaymentAccounts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
