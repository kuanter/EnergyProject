using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyProject.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CardDatas_Addresses_AddressId",
                table: "CardDatas");

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "CardDatas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses",
                column: "PaymentAccountId",
                principalTable: "PaymentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardDatas_Addresses_AddressId",
                table: "CardDatas",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CardDatas_Addresses_AddressId",
                table: "CardDatas");

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "CardDatas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PaymentAccounts_PaymentAccountId",
                table: "Addresses",
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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
