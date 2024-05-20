using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Command.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldValueTotalBeforeServiceCharge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceChage",
                table: "Command");

            migrationBuilder.RenameColumn(
                name: "ValueTotal",
                table: "Command",
                newName: "ValueTotalBeforeServiceCharge");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueTotalBeforeServiceCharge",
                table: "Command",
                newName: "ValueTotal");

            migrationBuilder.AddColumn<decimal>(
                name: "ServiceChage",
                table: "Command",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
