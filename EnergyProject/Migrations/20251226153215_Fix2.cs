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
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PaymentAccountId",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentAccountId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PaymentAccountId",
                table: "Addresses",
                column: "PaymentAccountId",
                unique: true,
                filter: "[PaymentAccountId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses",
                column: "PaymentAccountId",
                principalTable: "PaymentAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PaymentAccountId",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentAccountId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PaymentAccountId",
                table: "Addresses",
                column: "PaymentAccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses",
                column: "PaymentAccountId",
                principalTable: "PaymentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
