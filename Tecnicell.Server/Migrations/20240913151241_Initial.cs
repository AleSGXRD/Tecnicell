using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnicell.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accessory_type",
                columns: table => new
                {
                    accessorytypecode = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("accessory_type_pkey", x => x.accessorytypecode);
                });

            migrationBuilder.CreateTable(
                name: "action_history",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("action_history_pkey", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "battery_brand",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying", nullable: false),
                    description = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("battery_brand_pkey", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "branch",
                columns: table => new
                {
                    branchcode = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("branch_pkey", x => x.branchcode);
                });

            migrationBuilder.CreateTable(
                name: "currency",
                columns: table => new
                {
                    currencycode = table.Column<string>(type: "character varying", nullable: false),
                    currencyname = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("currency_pkey", x => x.currencycode);
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    imagecode = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    file = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("image_pkey", x => x.imagecode);
                });

            migrationBuilder.CreateTable(
                name: "phone_brand",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying", nullable: false),
                    description = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("phone_brand_pkey", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    rolecode = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("role_pkey", x => x.rolecode);
                });

            migrationBuilder.CreateTable(
                name: "accessory",
                columns: table => new
                {
                    accessorycode = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    accessory_type = table.Column<string>(type: "character varying", nullable: true),
                    sale_price = table.Column<decimal>(type: "numeric", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("accessory_pkey", x => x.accessorycode);
                    table.ForeignKey(
                        name: "accessory_accessory_type_fkey",
                        column: x => x.accessory_type,
                        principalTable: "accessory_type",
                        principalColumn: "accessorytypecode");
                });

            migrationBuilder.CreateTable(
                name: "battery",
                columns: table => new
                {
                    batterycode = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    brand = table.Column<string>(type: "character varying", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    sale_price = table.Column<decimal>(type: "numeric", nullable: true),
                    warranty = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("battery_pkey", x => x.batterycode);
                    table.ForeignKey(
                        name: "battery_brand_fkey",
                        column: x => x.brand,
                        principalTable: "battery_brand",
                        principalColumn: "name");
                });

            migrationBuilder.CreateTable(
                name: "sale",
                columns: table => new
                {
                    salecode = table.Column<string>(type: "character varying", nullable: false),
                    currencycode = table.Column<string>(type: "character varying", nullable: true),
                    warranty = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    cost = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sale_pkey", x => x.salecode);
                    table.ForeignKey(
                        name: "sale_currencycode_fkey",
                        column: x => x.currencycode,
                        principalTable: "currency",
                        principalColumn: "currencycode");
                });

            migrationBuilder.CreateTable(
                name: "phone",
                columns: table => new
                {
                    imei = table.Column<string>(type: "character varying", nullable: false),
                    brand = table.Column<string>(type: "character varying", nullable: true),
                    sale_price = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("phone_pkey", x => x.imei);
                    table.ForeignKey(
                        name: "phone_brand_fkey",
                        column: x => x.brand,
                        principalTable: "phone_brand",
                        principalColumn: "name");
                });

            migrationBuilder.CreateTable(
                name: "phone_repair",
                columns: table => new
                {
                    imei = table.Column<string>(type: "character varying", nullable: false),
                    brand = table.Column<string>(type: "character varying", nullable: true),
                    customer_name = table.Column<string>(type: "character varying", nullable: true),
                    customer_id = table.Column<string>(type: "character varying", nullable: true),
                    customer_number = table.Column<string>(type: "character varying", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("phone_repair_pkey", x => x.imei);
                    table.ForeignKey(
                        name: "phone_repair_brand_fkey",
                        column: x => x.brand,
                        principalTable: "phone_brand",
                        principalColumn: "name");
                });

            migrationBuilder.CreateTable(
                name: "screen",
                columns: table => new
                {
                    screencode = table.Column<string>(type: "character varying", nullable: false),
                    brand = table.Column<string>(type: "character varying", nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    width = table.Column<decimal>(type: "numeric", nullable: true),
                    height = table.Column<decimal>(type: "numeric", nullable: true),
                    sale_price = table.Column<decimal>(type: "numeric", nullable: true),
                    warranty = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("screen_pkey", x => x.screencode);
                    table.ForeignKey(
                        name: "screen_brand_fkey",
                        column: x => x.brand,
                        principalTable: "phone_brand",
                        principalColumn: "name");
                });

            migrationBuilder.CreateTable(
                name: "user_account",
                columns: table => new
                {
                    usercode = table.Column<string>(type: "character varying", nullable: false),
                    role = table.Column<string>(type: "character varying", nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    password = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_account_pkey", x => x.usercode);
                    table.ForeignKey(
                        name: "user_account_role_fkey",
                        column: x => x.role,
                        principalTable: "role",
                        principalColumn: "rolecode");
                });

            migrationBuilder.CreateTable(
                name: "accessory_history",
                columns: table => new
                {
                    accessorycode = table.Column<string>(type: "character varying", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    action_history = table.Column<string>(type: "character varying", nullable: true),
                    tobranch = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    salecode = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("accessory_history_pkey", x => new { x.accessorycode, x.date });
                    table.ForeignKey(
                        name: "accessory_history_accessorycode_fkey",
                        column: x => x.accessorycode,
                        principalTable: "accessory",
                        principalColumn: "accessorycode");
                    table.ForeignKey(
                        name: "accessory_history_action_history_fkey",
                        column: x => x.action_history,
                        principalTable: "action_history",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "accessory_history_salecode_fkey",
                        column: x => x.salecode,
                        principalTable: "sale",
                        principalColumn: "salecode");
                    table.ForeignKey(
                        name: "accessory_history_tobranch_fkey",
                        column: x => x.tobranch,
                        principalTable: "branch",
                        principalColumn: "branchcode");
                });

            migrationBuilder.CreateTable(
                name: "battery_history",
                columns: table => new
                {
                    batterycode = table.Column<string>(type: "character varying", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    action_history = table.Column<string>(type: "character varying", nullable: true),
                    tobranch = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    salecode = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("battery_history_pkey", x => new { x.batterycode, x.date });
                    table.ForeignKey(
                        name: "battery_history_action_history_fkey",
                        column: x => x.action_history,
                        principalTable: "action_history",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "battery_history_batterycode_fkey",
                        column: x => x.batterycode,
                        principalTable: "battery",
                        principalColumn: "batterycode");
                    table.ForeignKey(
                        name: "battery_history_salecode_fkey",
                        column: x => x.salecode,
                        principalTable: "sale",
                        principalColumn: "salecode");
                    table.ForeignKey(
                        name: "battery_history_tobranch_fkey",
                        column: x => x.tobranch,
                        principalTable: "branch",
                        principalColumn: "branchcode");
                });

            migrationBuilder.CreateTable(
                name: "phone_history",
                columns: table => new
                {
                    imei = table.Column<string>(type: "character varying", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    action_history = table.Column<string>(type: "character varying", nullable: true),
                    tobranch = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    salecode = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("phone_history_pkey", x => new { x.imei, x.date });
                    table.ForeignKey(
                        name: "phone_history_action_history_fkey",
                        column: x => x.action_history,
                        principalTable: "action_history",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "phone_history_imei_fkey",
                        column: x => x.imei,
                        principalTable: "phone",
                        principalColumn: "imei");
                    table.ForeignKey(
                        name: "phone_history_salecode_fkey",
                        column: x => x.salecode,
                        principalTable: "sale",
                        principalColumn: "salecode");
                    table.ForeignKey(
                        name: "phone_history_tobranch_fkey",
                        column: x => x.tobranch,
                        principalTable: "branch",
                        principalColumn: "branchcode");
                });

            migrationBuilder.CreateTable(
                name: "phone_repair_history",
                columns: table => new
                {
                    imei = table.Column<string>(type: "character varying", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    action_history = table.Column<string>(type: "character varying", nullable: true),
                    tobranch = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    salecode = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("phone_repair_history_pkey", x => new { x.imei, x.date });
                    table.ForeignKey(
                        name: "phone_repair_history_action_history_fkey",
                        column: x => x.action_history,
                        principalTable: "action_history",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "phone_repair_history_imei_fkey",
                        column: x => x.imei,
                        principalTable: "phone_repair",
                        principalColumn: "imei");
                    table.ForeignKey(
                        name: "phone_repair_history_salecode_fkey",
                        column: x => x.salecode,
                        principalTable: "sale",
                        principalColumn: "salecode");
                    table.ForeignKey(
                        name: "phone_repair_history_tobranch_fkey",
                        column: x => x.tobranch,
                        principalTable: "branch",
                        principalColumn: "branchcode");
                });

            migrationBuilder.CreateTable(
                name: "screen_history",
                columns: table => new
                {
                    screencode = table.Column<string>(type: "character varying", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    action_history = table.Column<string>(type: "character varying", nullable: true),
                    tobranch = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    salecode = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("screen_history_pkey", x => new { x.screencode, x.date });
                    table.ForeignKey(
                        name: "screen_history_action_history_fkey",
                        column: x => x.action_history,
                        principalTable: "action_history",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "screen_history_salecode_fkey",
                        column: x => x.salecode,
                        principalTable: "sale",
                        principalColumn: "salecode");
                    table.ForeignKey(
                        name: "screen_history_screencode_fkey",
                        column: x => x.screencode,
                        principalTable: "screen",
                        principalColumn: "screencode");
                    table.ForeignKey(
                        name: "screen_history_tobranch_fkey",
                        column: x => x.tobranch,
                        principalTable: "branch",
                        principalColumn: "branchcode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_accessory_accessory_type",
                table: "accessory",
                column: "accessory_type");

            migrationBuilder.CreateIndex(
                name: "IX_accessory_history_action_history",
                table: "accessory_history",
                column: "action_history");

            migrationBuilder.CreateIndex(
                name: "IX_accessory_history_salecode",
                table: "accessory_history",
                column: "salecode");

            migrationBuilder.CreateIndex(
                name: "IX_accessory_history_tobranch",
                table: "accessory_history",
                column: "tobranch");

            migrationBuilder.CreateIndex(
                name: "IX_battery_brand",
                table: "battery",
                column: "brand");

            migrationBuilder.CreateIndex(
                name: "IX_battery_history_action_history",
                table: "battery_history",
                column: "action_history");

            migrationBuilder.CreateIndex(
                name: "IX_battery_history_salecode",
                table: "battery_history",
                column: "salecode");

            migrationBuilder.CreateIndex(
                name: "IX_battery_history_tobranch",
                table: "battery_history",
                column: "tobranch");

            migrationBuilder.CreateIndex(
                name: "IX_phone_brand",
                table: "phone",
                column: "brand");

            migrationBuilder.CreateIndex(
                name: "IX_phone_history_action_history",
                table: "phone_history",
                column: "action_history");

            migrationBuilder.CreateIndex(
                name: "IX_phone_history_salecode",
                table: "phone_history",
                column: "salecode");

            migrationBuilder.CreateIndex(
                name: "IX_phone_history_tobranch",
                table: "phone_history",
                column: "tobranch");

            migrationBuilder.CreateIndex(
                name: "IX_phone_repair_brand",
                table: "phone_repair",
                column: "brand");

            migrationBuilder.CreateIndex(
                name: "IX_phone_repair_history_action_history",
                table: "phone_repair_history",
                column: "action_history");

            migrationBuilder.CreateIndex(
                name: "IX_phone_repair_history_salecode",
                table: "phone_repair_history",
                column: "salecode");

            migrationBuilder.CreateIndex(
                name: "IX_phone_repair_history_tobranch",
                table: "phone_repair_history",
                column: "tobranch");

            migrationBuilder.CreateIndex(
                name: "IX_sale_currencycode",
                table: "sale",
                column: "currencycode");

            migrationBuilder.CreateIndex(
                name: "IX_screen_brand",
                table: "screen",
                column: "brand");

            migrationBuilder.CreateIndex(
                name: "IX_screen_history_action_history",
                table: "screen_history",
                column: "action_history");

            migrationBuilder.CreateIndex(
                name: "IX_screen_history_salecode",
                table: "screen_history",
                column: "salecode");

            migrationBuilder.CreateIndex(
                name: "IX_screen_history_tobranch",
                table: "screen_history",
                column: "tobranch");

            migrationBuilder.CreateIndex(
                name: "IX_user_account_role",
                table: "user_account",
                column: "role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accessory_history");

            migrationBuilder.DropTable(
                name: "battery_history");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "phone_history");

            migrationBuilder.DropTable(
                name: "phone_repair_history");

            migrationBuilder.DropTable(
                name: "screen_history");

            migrationBuilder.DropTable(
                name: "user_account");

            migrationBuilder.DropTable(
                name: "accessory");

            migrationBuilder.DropTable(
                name: "battery");

            migrationBuilder.DropTable(
                name: "phone");

            migrationBuilder.DropTable(
                name: "phone_repair");

            migrationBuilder.DropTable(
                name: "action_history");

            migrationBuilder.DropTable(
                name: "sale");

            migrationBuilder.DropTable(
                name: "screen");

            migrationBuilder.DropTable(
                name: "branch");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "accessory_type");

            migrationBuilder.DropTable(
                name: "battery_brand");

            migrationBuilder.DropTable(
                name: "currency");

            migrationBuilder.DropTable(
                name: "phone_brand");
        }
    }
}
