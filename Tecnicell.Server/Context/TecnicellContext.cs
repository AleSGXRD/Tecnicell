using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Context;

public partial class TecnicellContext : DbContext
{
    public TecnicellContext()
    {
    }

    public TecnicellContext(DbContextOptions<TecnicellContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accessory> Accessories { get; set; }

    public virtual DbSet<AccessoryHistory> AccessoryHistories { get; set; }

    public virtual DbSet<AccessoryType> AccessoryTypes { get; set; }

    public virtual DbSet<ActionHistory> ActionHistories { get; set; }

    public virtual DbSet<Battery> Batteries { get; set; }

    public virtual DbSet<BatteryBrand> BatteryBrands { get; set; }

    public virtual DbSet<BatteryHistory> BatteryHistories { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<PhoneBrand> PhoneBrands { get; set; }

    public virtual DbSet<PhoneHistory> PhoneHistories { get; set; }

    public virtual DbSet<PhoneRepair> PhoneRepairs { get; set; }

    public virtual DbSet<PhoneRepairHistory> PhoneRepairHistories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Screen> Screens { get; set; }

    public virtual DbSet<ScreenHistory> ScreenHistories { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;port=5432;Username=postgres;Password=123123;Database=tecnicell");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessory>(entity =>
        {
            entity.HasKey(e => e.AccessoryCode).HasName("accessory_pkey");

            entity.ToTable("accessory");

            entity.Property(e => e.AccessoryCode)
                .HasColumnType("character varying")
                .HasColumnName("accessory_code");
            entity.Property(e => e.AccessoryType)
                .HasColumnType("character varying")
                .HasColumnName("accessory_type");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");

            entity.HasOne(d => d.AccessoryTypeNavigation).WithMany(p => p.Accessories)
                .HasForeignKey(d => d.AccessoryType)
                .HasConstraintName("accessory_accessory_type_fkey");
        });

        modelBuilder.Entity<AccessoryHistory>(entity =>
        {
            entity.HasKey(e => new { e.AccessoryCode, e.Date }).HasName("accessory_history_pkey");

            entity.ToTable("accessory_history");

            entity.Property(e => e.AccessoryCode)
                .HasColumnType("character varying")
                .HasColumnName("accessory_code");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ActionHistory)
                .HasColumnType("character varying")
                .HasColumnName("action_history");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SaleCode)
                .HasColumnType("character varying")
                .HasColumnName("sale_code");
            entity.Property(e => e.ToBranch)
                .HasColumnType("character varying")
                .HasColumnName("to_branch");

            entity.HasOne(d => d.AccessoryCodeNavigation).WithMany(p => p.AccessoryHistories)
                .HasForeignKey(d => d.AccessoryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("accessory_history_accessory_code_fkey");

            entity.HasOne(d => d.ActionHistoryNavigation).WithMany(p => p.AccessoryHistories)
                .HasForeignKey(d => d.ActionHistory)
                .HasConstraintName("accessory_history_action_history_fkey");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.AccessoryHistories)
                .HasForeignKey(d => d.SaleCode)
                .HasConstraintName("accessory_history_sale_code_fkey");

            entity.HasOne(d => d.ToBranchNavigation).WithMany(p => p.AccessoryHistories)
                .HasForeignKey(d => d.ToBranch)
                .HasConstraintName("accessory_history_to_branch_fkey");
        });

        modelBuilder.Entity<AccessoryType>(entity =>
        {
            entity.HasKey(e => e.AccessoryTypeCode).HasName("accessory_type_pkey");

            entity.ToTable("accessory_type");

            entity.Property(e => e.AccessoryTypeCode)
                .HasColumnType("character varying")
                .HasColumnName("accessory_type_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<ActionHistory>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("action_history_pkey");

            entity.ToTable("action_history");

            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Battery>(entity =>
        {
            entity.HasKey(e => e.BatteryCode).HasName("battery_pkey");

            entity.ToTable("battery");

            entity.Property(e => e.BatteryCode)
                .HasColumnType("character varying")
                .HasColumnName("battery_code");
            entity.Property(e => e.Brand)
                .HasColumnType("character varying")
                .HasColumnName("brand");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.Warranty).HasColumnName("warranty");

            entity.HasOne(d => d.BrandNavigation).WithMany(p => p.Batteries)
                .HasForeignKey(d => d.Brand)
                .HasConstraintName("battery_brand_fkey");
        });

        modelBuilder.Entity<BatteryBrand>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("battery_brand_pkey");

            entity.ToTable("battery_brand");

            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
        });

        modelBuilder.Entity<BatteryHistory>(entity =>
        {
            entity.HasKey(e => new { e.BatteryCode, e.Date }).HasName("battery_history_pkey");

            entity.ToTable("battery_history");

            entity.Property(e => e.BatteryCode)
                .HasColumnType("character varying")
                .HasColumnName("battery_code");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ActionHistory)
                .HasColumnType("character varying")
                .HasColumnName("action_history");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SaleCode)
                .HasColumnType("character varying")
                .HasColumnName("sale_code");
            entity.Property(e => e.ToBranch)
                .HasColumnType("character varying")
                .HasColumnName("to_branch");

            entity.HasOne(d => d.ActionHistoryNavigation).WithMany(p => p.BatteryHistories)
                .HasForeignKey(d => d.ActionHistory)
                .HasConstraintName("battery_history_action_history_fkey");

            entity.HasOne(d => d.BatteryCodeNavigation).WithMany(p => p.BatteryHistories)
                .HasForeignKey(d => d.BatteryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battery_history_battery_code_fkey");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.BatteryHistories)
                .HasForeignKey(d => d.SaleCode)
                .HasConstraintName("battery_history_sale_code_fkey");

            entity.HasOne(d => d.ToBranchNavigation).WithMany(p => p.BatteryHistories)
                .HasForeignKey(d => d.ToBranch)
                .HasConstraintName("battery_history_to_branch_fkey");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchCode).HasName("branch_pkey");

            entity.ToTable("branch");

            entity.Property(e => e.BranchCode)
                .HasColumnType("character varying")
                .HasColumnName("branch_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyCode).HasName("currency_pkey");

            entity.ToTable("currency");

            entity.Property(e => e.CurrencyCode)
                .HasColumnType("character varying")
                .HasColumnName("currency_code");
            entity.Property(e => e.CurrencyName)
                .HasColumnType("character varying")
                .HasColumnName("currency_name");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Imagecode).HasName("image_pkey");

            entity.ToTable("image");

            entity.Property(e => e.Imagecode)
                .HasColumnType("character varying")
                .HasColumnName("imagecode");
            entity.Property(e => e.File).HasColumnName("file");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.Imei).HasName("phone_pkey");

            entity.ToTable("phone");

            entity.Property(e => e.Imei)
                .HasColumnType("character varying")
                .HasColumnName("imei");
            entity.Property(e => e.Brand)
                .HasColumnType("character varying")
                .HasColumnName("brand");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");

            entity.HasOne(d => d.BrandNavigation).WithMany(p => p.Phones)
                .HasForeignKey(d => d.Brand)
                .HasConstraintName("phone_brand_fkey");
        });

        modelBuilder.Entity<PhoneBrand>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("phone_brand_pkey");

            entity.ToTable("phone_brand");

            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
        });

        modelBuilder.Entity<PhoneHistory>(entity =>
        {
            entity.HasKey(e => new { e.Imei, e.Date }).HasName("phone_history_pkey");

            entity.ToTable("phone_history");

            entity.Property(e => e.Imei)
                .HasColumnType("character varying")
                .HasColumnName("imei");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ActionHistory)
                .HasColumnType("character varying")
                .HasColumnName("action_history");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.SaleCode)
                .HasColumnType("character varying")
                .HasColumnName("sale_code");
            entity.Property(e => e.ToBranch)
                .HasColumnType("character varying")
                .HasColumnName("to_branch");

            entity.HasOne(d => d.ActionHistoryNavigation).WithMany(p => p.PhoneHistories)
                .HasForeignKey(d => d.ActionHistory)
                .HasConstraintName("phone_history_action_history_fkey");

            entity.HasOne(d => d.ImeiNavigation).WithMany(p => p.PhoneHistories)
                .HasForeignKey(d => d.Imei)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("phone_history_imei_fkey");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.PhoneHistories)
                .HasForeignKey(d => d.SaleCode)
                .HasConstraintName("phone_history_sale_code_fkey");

            entity.HasOne(d => d.ToBranchNavigation).WithMany(p => p.PhoneHistories)
                .HasForeignKey(d => d.ToBranch)
                .HasConstraintName("phone_history_to_branch_fkey");
        });

        modelBuilder.Entity<PhoneRepair>(entity =>
        {
            entity.HasKey(e => e.Imei).HasName("phone_repair_pkey");

            entity.ToTable("phone_repair");

            entity.Property(e => e.Imei)
                .HasColumnType("character varying")
                .HasColumnName("imei");
            entity.Property(e => e.Brand)
                .HasColumnType("character varying")
                .HasColumnName("brand");
            entity.Property(e => e.CustomerId)
                .HasColumnType("character varying")
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasColumnType("character varying")
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerNumber)
                .HasColumnType("character varying")
                .HasColumnName("customer_number");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.BrandNavigation).WithMany(p => p.PhoneRepairs)
                .HasForeignKey(d => d.Brand)
                .HasConstraintName("phone_repair_brand_fkey");
        });

        modelBuilder.Entity<PhoneRepairHistory>(entity =>
        {
            entity.HasKey(e => new { e.Imei, e.Date }).HasName("phone_repair_history_pkey");

            entity.ToTable("phone_repair_history");

            entity.Property(e => e.Imei)
                .HasColumnType("character varying")
                .HasColumnName("imei");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ActionHistory)
                .HasColumnType("character varying")
                .HasColumnName("action_history");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.SaleCode)
                .HasColumnType("character varying")
                .HasColumnName("sale_code");
            entity.Property(e => e.ToBranch)
                .HasColumnType("character varying")
                .HasColumnName("to_branch");

            entity.HasOne(d => d.ActionHistoryNavigation).WithMany(p => p.PhoneRepairHistories)
                .HasForeignKey(d => d.ActionHistory)
                .HasConstraintName("phone_repair_history_action_history_fkey");

            entity.HasOne(d => d.ImeiNavigation).WithMany(p => p.PhoneRepairHistories)
                .HasForeignKey(d => d.Imei)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("phone_repair_history_imei_fkey");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.PhoneRepairHistories)
                .HasForeignKey(d => d.SaleCode)
                .HasConstraintName("phone_repair_history_sale_code_fkey");

            entity.HasOne(d => d.ToBranchNavigation).WithMany(p => p.PhoneRepairHistories)
                .HasForeignKey(d => d.ToBranch)
                .HasConstraintName("phone_repair_history_to_branch_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleCode).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.RoleCode)
                .HasColumnType("character varying")
                .HasColumnName("role_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleCode).HasName("sale_pkey");

            entity.ToTable("sale");

            entity.Property(e => e.SaleCode)
                .HasColumnType("character varying")
                .HasColumnName("sale_code");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.CurrencyCode)
                .HasColumnType("character varying")
                .HasColumnName("currency_code");
            entity.Property(e => e.Warranty)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("warranty");

            entity.HasOne(d => d.CurrencyCodeNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CurrencyCode)
                .HasConstraintName("sale_currency_code_fkey");
        });

        modelBuilder.Entity<Screen>(entity =>
        {
            entity.HasKey(e => e.ScreenCode).HasName("screen_pkey");

            entity.ToTable("screen");

            entity.Property(e => e.ScreenCode)
                .HasColumnType("character varying")
                .HasColumnName("screen_code");
            entity.Property(e => e.Brand)
                .HasColumnType("character varying")
                .HasColumnName("brand");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.Warranty).HasColumnName("warranty");
            entity.Property(e => e.Width).HasColumnName("width");

            entity.HasOne(d => d.BrandNavigation).WithMany(p => p.Screens)
                .HasForeignKey(d => d.Brand)
                .HasConstraintName("screen_brand_fkey");
        });

        modelBuilder.Entity<ScreenHistory>(entity =>
        {
            entity.HasKey(e => new { e.ScreenCode, e.Date }).HasName("screen_history_pkey");

            entity.ToTable("screen_history");

            entity.Property(e => e.ScreenCode)
                .HasColumnType("character varying")
                .HasColumnName("screen_code");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ActionHistory)
                .HasColumnType("character varying")
                .HasColumnName("action_history");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SaleCode)
                .HasColumnType("character varying")
                .HasColumnName("sale_code");
            entity.Property(e => e.ToBranch)
                .HasColumnType("character varying")
                .HasColumnName("to_branch");

            entity.HasOne(d => d.ActionHistoryNavigation).WithMany(p => p.ScreenHistories)
                .HasForeignKey(d => d.ActionHistory)
                .HasConstraintName("screen_history_action_history_fkey");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.ScreenHistories)
                .HasForeignKey(d => d.SaleCode)
                .HasConstraintName("screen_history_sale_code_fkey");

            entity.HasOne(d => d.ScreenCodeNavigation).WithMany(p => p.ScreenHistories)
                .HasForeignKey(d => d.ScreenCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("screen_history_screen_code_fkey");

            entity.HasOne(d => d.ToBranchNavigation).WithMany(p => p.ScreenHistories)
                .HasForeignKey(d => d.ToBranch)
                .HasConstraintName("screen_history_to_branch_fkey");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserCode).HasName("user_account_pkey");

            entity.ToTable("user_account");

            entity.Property(e => e.UserCode)
                .HasColumnType("character varying")
                .HasColumnName("user_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasColumnType("character varying")
                .HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("user_account_role_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
