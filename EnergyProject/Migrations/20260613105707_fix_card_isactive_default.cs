using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyProject.Migrations
{
    /// <inheritdoc />
    public partial class fix_card_isactive_default : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [CardDatas] SET [IsActive] = 1 WHERE [IsActive] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
