using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyProject.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_CardDatas_CardDataId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PaymentAccounts_PaymentAccountId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_CardDatas_Addresses_AddressId",
                table: "CardDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_CardDatas_AspNetUsers_UserId",
                table: "CardDatas");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_CardDatas_CardDataId",
                table: "Bills",
                column: "CardDataId",
                principalTable: "CardDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PaymentAccounts_PaymentAccountId",
                table: "Bills",
                column: "PaymentAccountId",
                principalTable: "PaymentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardDatas_Addresses_AddressId",
                table: "CardDatas",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardDatas_AspNetUsers_UserId",
                table: "CardDatas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_CardDatas_CardDataId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PaymentAccounts_PaymentAccountId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_CardDatas_Addresses_AddressId",
                table: "CardDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_CardDatas_AspNetUsers_UserId",
                table: "CardDatas");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_CardDatas_CardDataId",
                table: "Bills",
                column: "CardDataId",
                principalTable: "CardDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PaymentAccounts_PaymentAccountId",
                table: "Bills",
                column: "PaymentAccountId",
                principalTable: "PaymentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardDatas_Addresses_AddressId",
                table: "CardDatas",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CardDatas_AspNetUsers_UserId",
                table: "CardDatas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
