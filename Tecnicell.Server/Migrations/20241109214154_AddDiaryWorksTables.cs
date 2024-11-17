using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnicell.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddDiaryWorksTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "supplier_code",
                table: "screen_history",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "supplier_code",
                table: "phone_repair_history",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "supplier_code",
                table: "phone_history",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "supplier_code",
                table: "battery_history",
                type: "character varying",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "supplier_code",
                table: "accessory_history",
                type: "character varying",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    supplier_code = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.supplier_code);
                });

            migrationBuilder.CreateTable(
                name: "work_type",
                columns: table => new
                {
                    accessory_code = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_type", x => x.accessory_code);
                });

            migrationBuilder.CreateTable(
                name: "diary_work",
                columns: table => new
                {
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WorkType = table.Column<string>(type: "character varying", nullable: false),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    sale_code = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diary_work", x => x.date);
                    table.ForeignKey(
                        name: "FK_Dairy_Work_Work_Type",
                        column: x => x.WorkType,
                        principalTable: "work_type",
                        principalColumn: "accessory_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "dairy_work_sale_code_fkey",
                        column: x => x.sale_code,
                        principalTable: "sale",
                        principalColumn: "sale_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_screen_history_supplier_code",
                table: "screen_history",
                column: "supplier_code");

            migrationBuilder.CreateIndex(
                name: "IX_phone_repair_history_supplier_code",
                table: "phone_repair_history",
                column: "supplier_code");

            migrationBuilder.CreateIndex(
                name: "IX_phone_history_supplier_code",
                table: "phone_history",
                column: "supplier_code");

            migrationBuilder.CreateIndex(
                name: "IX_battery_history_supplier_code",
                table: "battery_history",
                column: "supplier_code");

            migrationBuilder.CreateIndex(
                name: "IX_accessory_history_supplier_code",
                table: "accessory_history",
                column: "supplier_code");

            migrationBuilder.CreateIndex(
                name: "IX_diary_work_sale_code",
                table: "diary_work",
                column: "sale_code");

            migrationBuilder.CreateIndex(
                name: "IX_diary_work_WorkType",
                table: "diary_work",
                column: "WorkType");

            migrationBuilder.AddForeignKey(
                name: "accessory_history_supplier_code_fkey",
                table: "accessory_history",
                column: "supplier_code",
                principalTable: "supplier",
                principalColumn: "supplier_code");

            migrationBuilder.AddForeignKey(
                name: "battery_history_supplier_code_fkey",
                table: "battery_history",
                column: "supplier_code",
                principalTable: "supplier",
                principalColumn: "supplier_code");

            migrationBuilder.AddForeignKey(
                name: "phone_history_supplier_code_fkey",
                table: "phone_history",
                column: "supplier_code",
                principalTable: "supplier",
                principalColumn: "supplier_code");

            migrationBuilder.AddForeignKey(
                name: "phone_repair_history_supplier_code_fkey",
                table: "phone_repair_history",
                column: "supplier_code",
                principalTable: "supplier",
                principalColumn: "supplier_code");

            migrationBuilder.AddForeignKey(
                name: "screen_history_supplier_code_fkey",
                table: "screen_history",
                column: "supplier_code",
                principalTable: "supplier",
                principalColumn: "supplier_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "accessory_history_supplier_code_fkey",
                table: "accessory_history");

            migrationBuilder.DropForeignKey(
                name: "battery_history_supplier_code_fkey",
                table: "battery_history");

            migrationBuilder.DropForeignKey(
                name: "phone_history_supplier_code_fkey",
                table: "phone_history");

            migrationBuilder.DropForeignKey(
                name: "phone_repair_history_supplier_code_fkey",
                table: "phone_repair_history");

            migrationBuilder.DropForeignKey(
                name: "screen_history_supplier_code_fkey",
                table: "screen_history");

            migrationBuilder.DropTable(
                name: "diary_work");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "work_type");

            migrationBuilder.DropIndex(
                name: "IX_screen_history_supplier_code",
                table: "screen_history");

            migrationBuilder.DropIndex(
                name: "IX_phone_repair_history_supplier_code",
                table: "phone_repair_history");

            migrationBuilder.DropIndex(
                name: "IX_phone_history_supplier_code",
                table: "phone_history");

            migrationBuilder.DropIndex(
                name: "IX_battery_history_supplier_code",
                table: "battery_history");

            migrationBuilder.DropIndex(
                name: "IX_accessory_history_supplier_code",
                table: "accessory_history");

            migrationBuilder.DropColumn(
                name: "supplier_code",
                table: "screen_history");

            migrationBuilder.DropColumn(
                name: "supplier_code",
                table: "phone_repair_history");

            migrationBuilder.DropColumn(
                name: "supplier_code",
                table: "phone_history");

            migrationBuilder.DropColumn(
                name: "supplier_code",
                table: "battery_history");

            migrationBuilder.DropColumn(
                name: "supplier_code",
                table: "accessory_history");
        }
    }
}
