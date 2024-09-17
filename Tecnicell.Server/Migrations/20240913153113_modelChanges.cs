using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnicell.Server.Migrations
{
    /// <inheritdoc />
    public partial class modelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "accessory_history_accessorycode_fkey",
                table: "accessory_history");

            migrationBuilder.DropForeignKey(
                name: "accessory_history_salecode_fkey",
                table: "accessory_history");

            migrationBuilder.DropForeignKey(
                name: "accessory_history_tobranch_fkey",
                table: "accessory_history");

            migrationBuilder.DropForeignKey(
                name: "battery_history_batterycode_fkey",
                table: "battery_history");

            migrationBuilder.DropForeignKey(
                name: "battery_history_salecode_fkey",
                table: "battery_history");

            migrationBuilder.DropForeignKey(
                name: "battery_history_tobranch_fkey",
                table: "battery_history");

            migrationBuilder.DropForeignKey(
                name: "phone_history_salecode_fkey",
                table: "phone_history");

            migrationBuilder.DropForeignKey(
                name: "phone_history_tobranch_fkey",
                table: "phone_history");

            migrationBuilder.DropForeignKey(
                name: "phone_repair_history_salecode_fkey",
                table: "phone_repair_history");

            migrationBuilder.DropForeignKey(
                name: "phone_repair_history_tobranch_fkey",
                table: "phone_repair_history");

            migrationBuilder.DropForeignKey(
                name: "sale_currencycode_fkey",
                table: "sale");

            migrationBuilder.DropForeignKey(
                name: "screen_history_salecode_fkey",
                table: "screen_history");

            migrationBuilder.DropForeignKey(
                name: "screen_history_screencode_fkey",
                table: "screen_history");

            migrationBuilder.DropForeignKey(
                name: "screen_history_tobranch_fkey",
                table: "screen_history");

            migrationBuilder.RenameColumn(
                name: "usercode",
                table: "user_account",
                newName: "user_code");

            migrationBuilder.RenameColumn(
                name: "tobranch",
                table: "screen_history",
                newName: "to_branch");

            migrationBuilder.RenameColumn(
                name: "salecode",
                table: "screen_history",
                newName: "sale_code");

            migrationBuilder.RenameColumn(
                name: "screencode",
                table: "screen_history",
                newName: "screen_code");

            migrationBuilder.RenameIndex(
                name: "IX_screen_history_tobranch",
                table: "screen_history",
                newName: "IX_screen_history_to_branch");

            migrationBuilder.RenameIndex(
                name: "IX_screen_history_salecode",
                table: "screen_history",
                newName: "IX_screen_history_sale_code");

            migrationBuilder.RenameColumn(
                name: "screencode",
                table: "screen",
                newName: "screen_code");

            migrationBuilder.RenameColumn(
                name: "currencycode",
                table: "sale",
                newName: "currency_code");

            migrationBuilder.RenameColumn(
                name: "salecode",
                table: "sale",
                newName: "sale_code");

            migrationBuilder.RenameIndex(
                name: "IX_sale_currencycode",
                table: "sale",
                newName: "IX_sale_currency_code");

            migrationBuilder.RenameColumn(
                name: "rolecode",
                table: "role",
                newName: "role_code");

            migrationBuilder.RenameColumn(
                name: "tobranch",
                table: "phone_repair_history",
                newName: "to_branch");

            migrationBuilder.RenameColumn(
                name: "salecode",
                table: "phone_repair_history",
                newName: "sale_code");

            migrationBuilder.RenameIndex(
                name: "IX_phone_repair_history_tobranch",
                table: "phone_repair_history",
                newName: "IX_phone_repair_history_to_branch");

            migrationBuilder.RenameIndex(
                name: "IX_phone_repair_history_salecode",
                table: "phone_repair_history",
                newName: "IX_phone_repair_history_sale_code");

            migrationBuilder.RenameColumn(
                name: "tobranch",
                table: "phone_history",
                newName: "to_branch");

            migrationBuilder.RenameColumn(
                name: "salecode",
                table: "phone_history",
                newName: "sale_code");

            migrationBuilder.RenameIndex(
                name: "IX_phone_history_tobranch",
                table: "phone_history",
                newName: "IX_phone_history_to_branch");

            migrationBuilder.RenameIndex(
                name: "IX_phone_history_salecode",
                table: "phone_history",
                newName: "IX_phone_history_sale_code");

            migrationBuilder.RenameColumn(
                name: "currencyname",
                table: "currency",
                newName: "currency_name");

            migrationBuilder.RenameColumn(
                name: "currencycode",
                table: "currency",
                newName: "currency_code");

            migrationBuilder.RenameColumn(
                name: "branchcode",
                table: "branch",
                newName: "branch_code");

            migrationBuilder.RenameColumn(
                name: "tobranch",
                table: "battery_history",
                newName: "to_branch");

            migrationBuilder.RenameColumn(
                name: "salecode",
                table: "battery_history",
                newName: "sale_code");

            migrationBuilder.RenameColumn(
                name: "batterycode",
                table: "battery_history",
                newName: "battery_code");

            migrationBuilder.RenameIndex(
                name: "IX_battery_history_tobranch",
                table: "battery_history",
                newName: "IX_battery_history_to_branch");

            migrationBuilder.RenameIndex(
                name: "IX_battery_history_salecode",
                table: "battery_history",
                newName: "IX_battery_history_sale_code");

            migrationBuilder.RenameColumn(
                name: "batterycode",
                table: "battery",
                newName: "battery_code");

            migrationBuilder.RenameColumn(
                name: "accessorytypecode",
                table: "accessory_type",
                newName: "accessory_type_code");

            migrationBuilder.RenameColumn(
                name: "tobranch",
                table: "accessory_history",
                newName: "to_branch");

            migrationBuilder.RenameColumn(
                name: "salecode",
                table: "accessory_history",
                newName: "sale_code");

            migrationBuilder.RenameColumn(
                name: "accessorycode",
                table: "accessory_history",
                newName: "accessory_code");

            migrationBuilder.RenameIndex(
                name: "IX_accessory_history_tobranch",
                table: "accessory_history",
                newName: "IX_accessory_history_to_branch");

            migrationBuilder.RenameIndex(
                name: "IX_accessory_history_salecode",
                table: "accessory_history",
                newName: "IX_accessory_history_sale_code");

            migrationBuilder.RenameColumn(
                name: "accessorycode",
                table: "accessory",
                newName: "accessory_code");

            migrationBuilder.AddForeignKey(
                name: "accessory_history_accessory_code_fkey",
                table: "accessory_history",
                column: "accessory_code",
                principalTable: "accessory",
                principalColumn: "accessory_code");

            migrationBuilder.AddForeignKey(
                name: "accessory_history_sale_code_fkey",
                table: "accessory_history",
                column: "sale_code",
                principalTable: "sale",
                principalColumn: "sale_code");

            migrationBuilder.AddForeignKey(
                name: "accessory_history_to_branch_fkey",
                table: "accessory_history",
                column: "to_branch",
                principalTable: "branch",
                principalColumn: "branch_code");

            migrationBuilder.AddForeignKey(
                name: "battery_history_battery_code_fkey",
                table: "battery_history",
                column: "battery_code",
                principalTable: "battery",
                principalColumn: "battery_code");

            migrationBuilder.AddForeignKey(
                name: "battery_history_sale_code_fkey",
                table: "battery_history",
                column: "sale_code",
                principalTable: "sale",
                principalColumn: "sale_code");

            migrationBuilder.AddForeignKey(
                name: "battery_history_to_branch_fkey",
                table: "battery_history",
                column: "to_branch",
                principalTable: "branch",
                principalColumn: "branch_code");

            migrationBuilder.AddForeignKey(
                name: "phone_history_sale_code_fkey",
                table: "phone_history",
                column: "sale_code",
                principalTable: "sale",
                principalColumn: "sale_code");

            migrationBuilder.AddForeignKey(
                name: "phone_history_to_branch_fkey",
                table: "phone_history",
                column: "to_branch",
                principalTable: "branch",
                principalColumn: "branch_code");

            migrationBuilder.AddForeignKey(
                name: "phone_repair_history_sale_code_fkey",
                table: "phone_repair_history",
                column: "sale_code",
                principalTable: "sale",
                principalColumn: "sale_code");

            migrationBuilder.AddForeignKey(
                name: "phone_repair_history_to_branch_fkey",
                table: "phone_repair_history",
                column: "to_branch",
                principalTable: "branch",
                principalColumn: "branch_code");

            migrationBuilder.AddForeignKey(
                name: "sale_currency_code_fkey",
                table: "sale",
                column: "currency_code",
                principalTable: "currency",
                principalColumn: "currency_code");

            migrationBuilder.AddForeignKey(
                name: "screen_history_sale_code_fkey",
                table: "screen_history",
                column: "sale_code",
                principalTable: "sale",
                principalColumn: "sale_code");

            migrationBuilder.AddForeignKey(
                name: "screen_history_screen_code_fkey",
                table: "screen_history",
                column: "screen_code",
                principalTable: "screen",
                principalColumn: "screen_code");

            migrationBuilder.AddForeignKey(
                name: "screen_history_to_branch_fkey",
                table: "screen_history",
                column: "to_branch",
                principalTable: "branch",
                principalColumn: "branch_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "accessory_history_accessory_code_fkey",
                table: "accessory_history");

            migrationBuilder.DropForeignKey(
                name: "accessory_history_sale_code_fkey",
                table: "accessory_history");

            migrationBuilder.DropForeignKey(
                name: "accessory_history_to_branch_fkey",
                table: "accessory_history");

            migrationBuilder.DropForeignKey(
                name: "battery_history_battery_code_fkey",
                table: "battery_history");

            migrationBuilder.DropForeignKey(
                name: "battery_history_sale_code_fkey",
                table: "battery_history");

            migrationBuilder.DropForeignKey(
                name: "battery_history_to_branch_fkey",
                table: "battery_history");

            migrationBuilder.DropForeignKey(
                name: "phone_history_sale_code_fkey",
                table: "phone_history");

            migrationBuilder.DropForeignKey(
                name: "phone_history_to_branch_fkey",
                table: "phone_history");

            migrationBuilder.DropForeignKey(
                name: "phone_repair_history_sale_code_fkey",
                table: "phone_repair_history");

            migrationBuilder.DropForeignKey(
                name: "phone_repair_history_to_branch_fkey",
                table: "phone_repair_history");

            migrationBuilder.DropForeignKey(
                name: "sale_currency_code_fkey",
                table: "sale");

            migrationBuilder.DropForeignKey(
                name: "screen_history_sale_code_fkey",
                table: "screen_history");

            migrationBuilder.DropForeignKey(
                name: "screen_history_screen_code_fkey",
                table: "screen_history");

            migrationBuilder.DropForeignKey(
                name: "screen_history_to_branch_fkey",
                table: "screen_history");

            migrationBuilder.RenameColumn(
                name: "user_code",
                table: "user_account",
                newName: "usercode");

            migrationBuilder.RenameColumn(
                name: "to_branch",
                table: "screen_history",
                newName: "tobranch");

            migrationBuilder.RenameColumn(
                name: "sale_code",
                table: "screen_history",
                newName: "salecode");

            migrationBuilder.RenameColumn(
                name: "screen_code",
                table: "screen_history",
                newName: "screencode");

            migrationBuilder.RenameIndex(
                name: "IX_screen_history_to_branch",
                table: "screen_history",
                newName: "IX_screen_history_tobranch");

            migrationBuilder.RenameIndex(
                name: "IX_screen_history_sale_code",
                table: "screen_history",
                newName: "IX_screen_history_salecode");

            migrationBuilder.RenameColumn(
                name: "screen_code",
                table: "screen",
                newName: "screencode");

            migrationBuilder.RenameColumn(
                name: "currency_code",
                table: "sale",
                newName: "currencycode");

            migrationBuilder.RenameColumn(
                name: "sale_code",
                table: "sale",
                newName: "salecode");

            migrationBuilder.RenameIndex(
                name: "IX_sale_currency_code",
                table: "sale",
                newName: "IX_sale_currencycode");

            migrationBuilder.RenameColumn(
                name: "role_code",
                table: "role",
                newName: "rolecode");

            migrationBuilder.RenameColumn(
                name: "to_branch",
                table: "phone_repair_history",
                newName: "tobranch");

            migrationBuilder.RenameColumn(
                name: "sale_code",
                table: "phone_repair_history",
                newName: "salecode");

            migrationBuilder.RenameIndex(
                name: "IX_phone_repair_history_to_branch",
                table: "phone_repair_history",
                newName: "IX_phone_repair_history_tobranch");

            migrationBuilder.RenameIndex(
                name: "IX_phone_repair_history_sale_code",
                table: "phone_repair_history",
                newName: "IX_phone_repair_history_salecode");

            migrationBuilder.RenameColumn(
                name: "to_branch",
                table: "phone_history",
                newName: "tobranch");

            migrationBuilder.RenameColumn(
                name: "sale_code",
                table: "phone_history",
                newName: "salecode");

            migrationBuilder.RenameIndex(
                name: "IX_phone_history_to_branch",
                table: "phone_history",
                newName: "IX_phone_history_tobranch");

            migrationBuilder.RenameIndex(
                name: "IX_phone_history_sale_code",
                table: "phone_history",
                newName: "IX_phone_history_salecode");

            migrationBuilder.RenameColumn(
                name: "currency_name",
                table: "currency",
                newName: "currencyname");

            migrationBuilder.RenameColumn(
                name: "currency_code",
                table: "currency",
                newName: "currencycode");

            migrationBuilder.RenameColumn(
                name: "branch_code",
                table: "branch",
                newName: "branchcode");

            migrationBuilder.RenameColumn(
                name: "to_branch",
                table: "battery_history",
                newName: "tobranch");

            migrationBuilder.RenameColumn(
                name: "sale_code",
                table: "battery_history",
                newName: "salecode");

            migrationBuilder.RenameColumn(
                name: "battery_code",
                table: "battery_history",
                newName: "batterycode");

            migrationBuilder.RenameIndex(
                name: "IX_battery_history_to_branch",
                table: "battery_history",
                newName: "IX_battery_history_tobranch");

            migrationBuilder.RenameIndex(
                name: "IX_battery_history_sale_code",
                table: "battery_history",
                newName: "IX_battery_history_salecode");

            migrationBuilder.RenameColumn(
                name: "battery_code",
                table: "battery",
                newName: "batterycode");

            migrationBuilder.RenameColumn(
                name: "accessory_type_code",
                table: "accessory_type",
                newName: "accessorytypecode");

            migrationBuilder.RenameColumn(
                name: "to_branch",
                table: "accessory_history",
                newName: "tobranch");

            migrationBuilder.RenameColumn(
                name: "sale_code",
                table: "accessory_history",
                newName: "salecode");

            migrationBuilder.RenameColumn(
                name: "accessory_code",
                table: "accessory_history",
                newName: "accessorycode");

            migrationBuilder.RenameIndex(
                name: "IX_accessory_history_to_branch",
                table: "accessory_history",
                newName: "IX_accessory_history_tobranch");

            migrationBuilder.RenameIndex(
                name: "IX_accessory_history_sale_code",
                table: "accessory_history",
                newName: "IX_accessory_history_salecode");

            migrationBuilder.RenameColumn(
                name: "accessory_code",
                table: "accessory",
                newName: "accessorycode");

            migrationBuilder.AddForeignKey(
                name: "accessory_history_accessorycode_fkey",
                table: "accessory_history",
                column: "accessorycode",
                principalTable: "accessory",
                principalColumn: "accessorycode");

            migrationBuilder.AddForeignKey(
                name: "accessory_history_salecode_fkey",
                table: "accessory_history",
                column: "salecode",
                principalTable: "sale",
                principalColumn: "salecode");

            migrationBuilder.AddForeignKey(
                name: "accessory_history_tobranch_fkey",
                table: "accessory_history",
                column: "tobranch",
                principalTable: "branch",
                principalColumn: "branchcode");

            migrationBuilder.AddForeignKey(
                name: "battery_history_batterycode_fkey",
                table: "battery_history",
                column: "batterycode",
                principalTable: "battery",
                principalColumn: "batterycode");

            migrationBuilder.AddForeignKey(
                name: "battery_history_salecode_fkey",
                table: "battery_history",
                column: "salecode",
                principalTable: "sale",
                principalColumn: "salecode");

            migrationBuilder.AddForeignKey(
                name: "battery_history_tobranch_fkey",
                table: "battery_history",
                column: "tobranch",
                principalTable: "branch",
                principalColumn: "branchcode");

            migrationBuilder.AddForeignKey(
                name: "phone_history_salecode_fkey",
                table: "phone_history",
                column: "salecode",
                principalTable: "sale",
                principalColumn: "salecode");

            migrationBuilder.AddForeignKey(
                name: "phone_history_tobranch_fkey",
                table: "phone_history",
                column: "tobranch",
                principalTable: "branch",
                principalColumn: "branchcode");

            migrationBuilder.AddForeignKey(
                name: "phone_repair_history_salecode_fkey",
                table: "phone_repair_history",
                column: "salecode",
                principalTable: "sale",
                principalColumn: "salecode");

            migrationBuilder.AddForeignKey(
                name: "phone_repair_history_tobranch_fkey",
                table: "phone_repair_history",
                column: "tobranch",
                principalTable: "branch",
                principalColumn: "branchcode");

            migrationBuilder.AddForeignKey(
                name: "sale_currencycode_fkey",
                table: "sale",
                column: "currencycode",
                principalTable: "currency",
                principalColumn: "currencycode");

            migrationBuilder.AddForeignKey(
                name: "screen_history_salecode_fkey",
                table: "screen_history",
                column: "salecode",
                principalTable: "sale",
                principalColumn: "salecode");

            migrationBuilder.AddForeignKey(
                name: "screen_history_screencode_fkey",
                table: "screen_history",
                column: "screencode",
                principalTable: "screen",
                principalColumn: "screencode");

            migrationBuilder.AddForeignKey(
                name: "screen_history_tobranch_fkey",
                table: "screen_history",
                column: "tobranch",
                principalTable: "branch",
                principalColumn: "branchcode");
        }
    }
}
