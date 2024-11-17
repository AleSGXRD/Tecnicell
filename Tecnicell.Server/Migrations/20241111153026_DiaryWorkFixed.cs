using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnicell.Server.Migrations
{
    /// <inheritdoc />
    public partial class DiaryWorkFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_phone_repair_history_supplier_code",
                table: "phone_repair_history");

            migrationBuilder.DropForeignKey(
                name: "phone_repair_history_supplier_code_fkey",
                table: "phone_repair_history");

            migrationBuilder.DropColumn(
                name: "supplier_code",
                table: "phone_repair_history");

            migrationBuilder.AddColumn<string>(
                name: "user_code",
                table: "diary_work",
                type: "character varying",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_diary_work_user_code",
                table: "diary_work",
                column: "user_code");

            migrationBuilder.AddForeignKey(
                name: "dairy_work_user_code_fkey",
                table: "diary_work",
                column: "user_code",
                principalTable: "user_info",
                principalColumn: "user_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "dairy_work_user_code_fkey",
                table: "diary_work");

            migrationBuilder.DropIndex(
                name: "IX_diary_work_user_code",
                table: "diary_work");

            migrationBuilder.DropColumn(
                name: "user_code",
                table: "diary_work");

            migrationBuilder.AddColumn<string>(
                name: "supplier_code",
                table: "phone_repair_history",
                type: "character varying",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_phone_repair_history_supplier_code",
                table: "phone_repair_history",
                column: "supplier_code");

            migrationBuilder.AddForeignKey(
                name: "phone_repair_history_supplier_code_fkey",
                table: "phone_repair_history",
                column: "supplier_code",
                principalTable: "supplier",
                principalColumn: "supplier_code");
        }
    }
}
