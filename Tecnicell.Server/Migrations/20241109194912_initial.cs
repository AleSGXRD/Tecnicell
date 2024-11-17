using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tecnicell.Server.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // accessory_type
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS accessory_type (
                accessory_type_code TEXT NOT NULL PRIMARY KEY,
                name TEXT
            );");

            // action_history
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS action_history (
                name TEXT NOT NULL PRIMARY KEY
            );");

            // branch
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS branch (
                branch_code TEXT NOT NULL PRIMARY KEY,
                name TEXT
            );");

            // brand
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS brand (
                name TEXT NOT NULL PRIMARY KEY,
                description TEXT
            );");

            // currency
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS currency (
                currency_code TEXT NOT NULL PRIMARY KEY,
                currency_name TEXT
            );");

            // image
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS image (
                image_code TEXT NOT NULL PRIMARY KEY,
                name TEXT,
                file BLOB
            );");

            // role
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS role (
                role_code TEXT NOT NULL PRIMARY KEY,
                name TEXT
            );");

            // searchs
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS searchs (
                date DATETIME NOT NULL PRIMARY KEY,
                value TEXT
            );");

            // usd
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS usd (
                date DATETIME NOT NULL PRIMARY KEY,
                value INTEGER
            );");

            // sale
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS sale (
                sale_code TEXT NOT NULL PRIMARY KEY,
                currency_code TEXT,
                warranty DATETIME,
                cost DECIMAL,
                FOREIGN KEY (currency_code) REFERENCES currency (currency_code)
            );");

            // accessory
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS accessory (
                accessory_code TEXT NOT NULL PRIMARY KEY,
                name TEXT,
                accessory_type TEXT,
                sale_price DECIMAL,
                image_code TEXT,
                FOREIGN KEY (accessory_type) REFERENCES accessory_type (accessory_type_code) ON DELETE CASCADE,
                FOREIGN KEY (image_code) REFERENCES image (image_code) ON DELETE CASCADE
            );");
            
        // accessory_type
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS accessory_type (
                accessory_type_code TEXT NOT NULL PRIMARY KEY,
                name TEXT
            );");

        // action_history
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS action_history (
                name TEXT NOT NULL PRIMARY KEY
            );");

        // branch
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS branch (
                branch_code TEXT NOT NULL PRIMARY KEY,
                name TEXT
            );");

        // brand
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS brand (
                name TEXT NOT NULL PRIMARY KEY,
                description TEXT
            );");

        // currency
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS currency (
                currency_code TEXT NOT NULL PRIMARY KEY,
                currency_name TEXT
            );");

        // image
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS image (
                image_code TEXT NOT NULL PRIMARY KEY,
                name TEXT,
                file BLOB
            );");

        // role
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS role (
                role_code TEXT NOT NULL PRIMARY KEY,
                name TEXT
            );");

        // searchs
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS searchs (
                date DATETIME NOT NULL PRIMARY KEY,
                value TEXT
            );");

        // usd
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS usd (
                date DATETIME NOT NULL PRIMARY KEY,
                value INTEGER
            );");

        // sale
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS sale (
                sale_code TEXT NOT NULL PRIMARY KEY,
                currency_code TEXT,
                warranty DATETIME,
                cost DECIMAL,
                FOREIGN KEY (currency_code) REFERENCES currency (currency_code)
            );");

        // accessory
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS accessory (
                accessory_code TEXT NOT NULL PRIMARY KEY,
                name TEXT,
                accessory_type TEXT,
                sale_price DECIMAL,
                image_code TEXT,
                FOREIGN KEY (accessory_type) REFERENCES accessory_type (accessory_type_code) ON DELETE CASCADE,
                FOREIGN KEY (image_code) REFERENCES image (image_code) ON DELETE CASCADE
            );");

        // battery
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS battery (
                battery_code TEXT NOT NULL PRIMARY KEY,
                name TEXT,
                brand TEXT,
                sale_price DECIMAL,
                image_code TEXT,
                warranty INTEGER,
                FOREIGN KEY (brand) REFERENCES brand (name),
                FOREIGN KEY (image_code) REFERENCES image (image_code) ON DELETE CASCADE
            );");

        // phone
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS phone (
                imei TEXT NOT NULL PRIMARY KEY,
                brand TEXT,
                name TEXT,
                sale_price DECIMAL,
                image_code TEXT,
                FOREIGN KEY (brand) REFERENCES brand (name),
                FOREIGN KEY (image_code) REFERENCES image (image_code) ON DELETE CASCADE
            );");

        // phone_repair
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS phone_repair (
                imei TEXT NOT NULL PRIMARY KEY,
                brand TEXT,
                name TEXT,
                customer_name TEXT,
                customer_id TEXT,
                customer_number TEXT,
                image_code TEXT,
                price DECIMAL,
                FOREIGN KEY (brand) REFERENCES brand (name),
                FOREIGN KEY (image_code) REFERENCES image (image_code) ON DELETE CASCADE
            );");

        // screen
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS screen (
                screen_code TEXT NOT NULL PRIMARY KEY,
                brand TEXT,
                name TEXT,
                width DECIMAL,
                height DECIMAL,
                sale_price DECIMAL,
                image_code TEXT,
                warranty INTEGER,
                FOREIGN KEY (brand) REFERENCES brand (name),
                FOREIGN KEY (image_code) REFERENCES image (image_code) ON DELETE CASCADE
            );");

        // user_info
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS user_info (
                user_code TEXT NOT NULL PRIMARY KEY,
                role TEXT,
                branch TEXT,
                image_code TEXT,
                name TEXT,
                FOREIGN KEY (branch) REFERENCES branch (branch_code),
                FOREIGN KEY (image_code) REFERENCES image (image_code) ON DELETE CASCADE,
                FOREIGN KEY (role) REFERENCES role (role_code)
            );");

        // accessory_history
        migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS accessory_history (
                accessory_code TEXT NOT NULL,
                user_code TEXT NOT NULL,
                date DATETIME NOT NULL,
                action_history TEXT,
                to_branch TEXT,
                description TEXT,
                quantity INTEGER,
                sale_code TEXT,
                PRIMARY KEY (accessory_code, date, user_code),
                FOREIGN KEY (accessory_code) REFERENCES accessory (accessory_code) ON DELETE CASCADE,
                FOREIGN KEY (action_history) REFERENCES action_history (name),
                FOREIGN KEY (sale_code) REFERENCES sale (sale_code) ON DELETE CASCADE,
                FOREIGN KEY (to_branch) REFERENCES branch (branch_code),
                FOREIGN KEY (user_code) REFERENCES user_info (user_code)
            );");


            // battery_history
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS battery_history (
                battery_code TEXT NOT NULL,
                user_code TEXT NOT NULL,
                date DATETIME NOT NULL,
                action_history TEXT,
                to_branch TEXT,
                description TEXT,
                quantity INTEGER,
                sale_code TEXT,
                PRIMARY KEY (battery_code, date, user_code),
                FOREIGN KEY (action_history) REFERENCES action_history (name),
                FOREIGN KEY (battery_code) REFERENCES battery (battery_code) ON DELETE CASCADE,
                FOREIGN KEY (sale_code) REFERENCES sale (sale_code) ON DELETE CASCADE,
                FOREIGN KEY (to_branch) REFERENCES branch (branch_code),
                FOREIGN KEY (user_code) REFERENCES user_info (user_code)
            );");

            // phone_history
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS phone_history (
                imei TEXT NOT NULL,
                user_code TEXT NOT NULL,
                date DATETIME NOT NULL,
                action_history TEXT,
                to_branch TEXT,
                description TEXT,
                sale_code TEXT,
                PRIMARY KEY (imei, date, user_code),
                FOREIGN KEY (action_history) REFERENCES action_history (name),
                FOREIGN KEY (imei) REFERENCES phone (imei) ON DELETE CASCADE,
                FOREIGN KEY (sale_code) REFERENCES sale (sale_code) ON DELETE CASCADE,
                FOREIGN KEY (to_branch) REFERENCES branch (branch_code),
                FOREIGN KEY (user_code) REFERENCES user_info (user_code)
            );");

            // phone_repair_history
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS phone_repair_history (
                imei TEXT NOT NULL,
                user_code TEXT NOT NULL,
                date DATETIME NOT NULL,
                action_history TEXT,
                to_branch TEXT,
                description TEXT,
                sale_code TEXT,
                PRIMARY KEY (imei, date, user_code),
                FOREIGN KEY (action_history) REFERENCES action_history (name),
                FOREIGN KEY (imei) REFERENCES phone_repair (imei) ON DELETE CASCADE,
                FOREIGN KEY (sale_code) REFERENCES sale (sale_code) ON DELETE CASCADE,
                FOREIGN KEY (to_branch) REFERENCES branch (branch_code),
                FOREIGN KEY (user_code) REFERENCES user_info (user_code)
            );");
            // screen_history
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS screen_history (
                screen_code TEXT NOT NULL,
                user_code TEXT NOT NULL,
                date DATETIME NOT NULL,
                action_history TEXT,
                to_branch TEXT,
                description TEXT,
                quantity INTEGER,
                sale_code TEXT,
                PRIMARY KEY (screen_code, date, user_code),
                FOREIGN KEY (action_history) REFERENCES action_history (name),
                FOREIGN KEY (sale_code) REFERENCES sale (sale_code) ON DELETE CASCADE,
                FOREIGN KEY (screen_code) REFERENCES screen (screen_code) ON DELETE CASCADE,
                FOREIGN KEY (to_branch) REFERENCES branch (branch_code),
                FOREIGN KEY (user_code) REFERENCES user_info (user_code)
            );");

            // user_account
            migrationBuilder.Sql(@"
            CREATE TABLE IF NOT EXISTS user_account (
                user_code TEXT NOT NULL,
                name TEXT,
                password TEXT,
                PRIMARY KEY (user_code),
                FOREIGN KEY (user_code) REFERENCES user_info (user_code) ON DELETE CASCADE
            );");

            // Crear las tablas y sus respectivas verificaciones...

            // Crear índices con verificación de existencia
            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_accessory_accessory_type ON accessory (accessory_type);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_accessory_image_code ON accessory (image_code);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_accessory_history_action_history ON accessory_history (action_history);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_accessory_history_sale_code ON accessory_history (sale_code);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_accessory_history_to_branch ON accessory_history (to_branch);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_accessory_history_user_code ON accessory_history (user_code);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_battery_brand ON battery (brand);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_battery_image_code ON battery (image_code);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_battery_history_action_history ON battery_history (action_history);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_battery_history_sale_code ON battery_history (sale_code);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_battery_history_to_branch ON battery_history (to_branch);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_battery_history_user_code ON battery_history (user_code);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_phone_brand ON phone (brand);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_phone_image_code ON phone (image_code);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_phone_history_action_history ON phone_history (action_history);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_phone_history_sale_code ON phone_history (sale_code);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_phone_history_to_branch ON phone_history (to_branch);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_phone_history_user_code ON phone_history (user_code);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_phone_repair_brand ON phone_repair (brand);");

            migrationBuilder.Sql(@"
        CREATE INDEX IF NOT EXISTS IX_phone_repair_image_code ON phone_repair (image_code);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_phone_repair_history_action_history ON phone_repair_history (action_history);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_phone_repair_history_sale_code ON phone_repair_history (sale_code);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_phone_repair_history_to_branch ON phone_repair_history (to_branch);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_phone_repair_history_user_code ON phone_repair_history (user_code);");

            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_sale_currency_code ON sale (currency_code);");

            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_screen_brand ON screen (brand);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_screen_image_code ON screen (image_code);");

            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_screen_history_action_history ON screen_history (action_history);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_screen_history_sale_code ON screen_history (sale_code);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_screen_history_to_branch ON screen_history (to_branch);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_screen_history_user_code ON screen_history (user_code);");

            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_user_info_branch ON user_info (branch);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_user_info_image_code ON user_info (image_code);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_user_info_role ON user_info (role);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accessory_history");

            migrationBuilder.DropTable(
                name: "battery_history");

            migrationBuilder.DropTable(
                name: "phone_history");

            migrationBuilder.DropTable(
                name: "phone_repair_history");

            migrationBuilder.DropTable(
                name: "screen_history");

            migrationBuilder.DropTable(
                name: "searchs");

            migrationBuilder.DropTable(
                name: "usd");

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
                name: "user_info");

            migrationBuilder.DropTable(
                name: "accessory_type");

            migrationBuilder.DropTable(
                name: "currency");

            migrationBuilder.DropTable(
                name: "brand");

            migrationBuilder.DropTable(
                name: "branch");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
