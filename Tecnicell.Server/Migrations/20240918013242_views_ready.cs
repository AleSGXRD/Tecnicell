using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnicell.Server.Migrations
{
    /// <inheritdoc />
    public partial class views_ready : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "screen");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "battery");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "accessory");

            migrationBuilder.AddColumn<string>(
                name: "image_code",
                table: "user_account",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image_code",
                table: "screen",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image_code",
                table: "phone_repair",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image_code",
                table: "phone",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image_code",
                table: "battery",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image_code",
                table: "accessory",
                type: "character varying",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_account_image_code",
                table: "user_account",
                column: "image_code");

            migrationBuilder.CreateIndex(
                name: "IX_screen_image_code",
                table: "screen",
                column: "image_code");

            migrationBuilder.CreateIndex(
                name: "IX_phone_repair_image_code",
                table: "phone_repair",
                column: "image_code");

            migrationBuilder.CreateIndex(
                name: "IX_phone_image_code",
                table: "phone",
                column: "image_code");

            migrationBuilder.CreateIndex(
                name: "IX_battery_image_code",
                table: "battery",
                column: "image_code");

            migrationBuilder.CreateIndex(
                name: "IX_accessory_image_code",
                table: "accessory",
                column: "image_code");

            migrationBuilder.AddForeignKey(
                name: "accessory_image_fkey",
                table: "accessory",
                column: "image_code",
                principalTable: "image",
                principalColumn: "imagecode");

            migrationBuilder.AddForeignKey(
                name: "battery_image_code_fkey",
                table: "battery",
                column: "image_code",
                principalTable: "image",
                principalColumn: "imagecode");

            migrationBuilder.AddForeignKey(
                name: "phone_image_fkey",
                table: "phone",
                column: "image_code",
                principalTable: "image",
                principalColumn: "imagecode");

            migrationBuilder.AddForeignKey(
                name: "phone_repair_image_fkey",
                table: "phone_repair",
                column: "image_code",
                principalTable: "image",
                principalColumn: "imagecode");

            migrationBuilder.AddForeignKey(
                name: "screen_image_fkey",
                table: "screen",
                column: "image_code",
                principalTable: "image",
                principalColumn: "imagecode");

            migrationBuilder.AddForeignKey(
                name: "user_account_image_fkey",
                table: "user_account",
                column: "image_code",
                principalTable: "image",
                principalColumn: "imagecode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "accessory_image_fkey",
                table: "accessory");

            migrationBuilder.DropForeignKey(
                name: "battery_image_code_fkey",
                table: "battery");

            migrationBuilder.DropForeignKey(
                name: "phone_image_fkey",
                table: "phone");

            migrationBuilder.DropForeignKey(
                name: "phone_repair_image_fkey",
                table: "phone_repair");

            migrationBuilder.DropForeignKey(
                name: "screen_image_fkey",
                table: "screen");

            migrationBuilder.DropForeignKey(
                name: "user_account_image_fkey",
                table: "user_account");

            migrationBuilder.DropIndex(
                name: "IX_user_account_image_code",
                table: "user_account");

            migrationBuilder.DropIndex(
                name: "IX_screen_image_code",
                table: "screen");

            migrationBuilder.DropIndex(
                name: "IX_phone_repair_image_code",
                table: "phone_repair");

            migrationBuilder.DropIndex(
                name: "IX_phone_image_code",
                table: "phone");

            migrationBuilder.DropIndex(
                name: "IX_battery_image_code",
                table: "battery");

            migrationBuilder.DropIndex(
                name: "IX_accessory_image_code",
                table: "accessory");

            migrationBuilder.DropColumn(
                name: "image_code",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "image_code",
                table: "screen");

            migrationBuilder.DropColumn(
                name: "image_code",
                table: "phone_repair");

            migrationBuilder.DropColumn(
                name: "image_code",
                table: "phone");

            migrationBuilder.DropColumn(
                name: "image_code",
                table: "battery");

            migrationBuilder.DropColumn(
                name: "image_code",
                table: "accessory");

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "screen",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "battery",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "accessory",
                type: "integer",
                nullable: true);
        }
    }
}
