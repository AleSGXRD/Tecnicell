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

    public virtual DbSet<AccessoryView> AccessoryViews { get; set; }

    public virtual DbSet<ActionHistory> ActionHistories { get; set; }

    public virtual DbSet<Battery> Batteries { get; set; }

    public virtual DbSet<BatteryHistory> BatteryHistories { get; set; }

    public virtual DbSet<BatteryView> BatteryViews { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<PhoneHistory> PhoneHistories { get; set; }

    public virtual DbSet<PhoneRepair> PhoneRepairs { get; set; }

    public virtual DbSet<PhoneRepairHistory> PhoneRepairHistories { get; set; }

    public virtual DbSet<PhoneRepairView> PhoneRepairViews { get; set; }

    public virtual DbSet<PhoneView> PhoneViews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Screen> Screens { get; set; }

    public virtual DbSet<ScreenHistory> ScreenHistories { get; set; }

    public virtual DbSet<ScreenView> ScreenViews { get; set; }

    public virtual DbSet<Search> Searchs { get; set; }

    public virtual DbSet<Usd> Usds { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

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
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");

            entity.HasOne(d => d.AccessoryTypeNavigation).WithMany(p => p.Accessories)
                .HasForeignKey(d => d.AccessoryType)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("accessory_accessory_type_fkey");

            entity.HasOne(d => d.ImageCodeNavigation).WithMany(p => p.Accessories)
                .HasForeignKey(d => d.ImageCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("accessory_image_code_fkey");
        });

        modelBuilder.Entity<AccessoryHistory>(entity =>
        {
            entity.HasKey(e => new { e.AccessoryCode, e.Date, e.UserCode }).HasName("accessory_history_pkey");

            entity.ToTable("accessory_history");

            entity.Property(e => e.AccessoryCode)
                .HasColumnType("character varying")
                .HasColumnName("accessory_code");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.UserCode)
                .HasColumnType("character varying")
                .HasColumnName("user_code");
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
                .HasConstraintName("accessory_history_accessory_code_fkey");

            entity.HasOne(d => d.ActionHistoryNavigation).WithMany(p => p.AccessoryHistories)
                .HasForeignKey(d => d.ActionHistory)
                .HasConstraintName("accessory_history_action_history_fkey");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.AccessoryHistories)
                .HasForeignKey(d => d.SaleCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("accessory_history_sale_code_fkey");

            entity.HasOne(d => d.ToBranchNavigation).WithMany(p => p.AccessoryHistories)
                .HasForeignKey(d => d.ToBranch)
                .HasConstraintName("accessory_history_to_branch_fkey");

            entity.HasOne(d => d.UserCodeNavigation).WithMany(p => p.AccessoryHistories)
                .HasForeignKey(d => d.UserCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("accessory_history_user_code_fkey");
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

        modelBuilder.Entity<AccessoryView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("accessory_view");

            entity.Property(e => e.Available).HasColumnName("available");
            entity.Property(e => e.Code)
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.TypeCode)
                .HasColumnType("character varying")
                .HasColumnName("type_code");
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
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.Warranty).HasColumnName("warranty");

            entity.HasOne(d => d.BrandNavigation).WithMany(p => p.Batteries)
                .HasForeignKey(d => d.Brand)
                .HasConstraintName("battery_brand_fkey");

            entity.HasOne(d => d.ImageCodeNavigation).WithMany(p => p.Batteries)
                .HasForeignKey(d => d.ImageCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("battery_image_code_fkey");
        });

        modelBuilder.Entity<BatteryHistory>(entity =>
        {
            entity.HasKey(e => new { e.BatteryCode, e.Date, e.UserCode }).HasName("battery_history_pkey");

            entity.ToTable("battery_history");

            entity.Property(e => e.BatteryCode)
                .HasColumnType("character varying")
                .HasColumnName("battery_code");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.UserCode)
                .HasColumnType("character varying")
                .HasColumnName("user_code");
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
                .HasConstraintName("battery_history_battery_code_fkey");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.BatteryHistories)
                .HasForeignKey(d => d.SaleCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("battery_history_sale_code_fkey");

            entity.HasOne(d => d.ToBranchNavigation).WithMany(p => p.BatteryHistories)
                .HasForeignKey(d => d.ToBranch)
                .HasConstraintName("battery_history_to_branch_fkey");

            entity.HasOne(d => d.UserCodeNavigation).WithMany(p => p.BatteryHistories)
                .HasForeignKey(d => d.UserCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battery_history_user_code_fkey");
        });

        modelBuilder.Entity<BatteryView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("battery_view");

            entity.Property(e => e.Available).HasColumnName("available");
            entity.Property(e => e.Code)
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.Warranty).HasColumnName("warranty");
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

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("brand_pkey");

            entity.ToTable("brand");

            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
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
            entity.HasKey(e => e.ImageCode).HasName("image_pkey");

            entity.ToTable("image");

            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
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
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");

            entity.HasOne(d => d.BrandNavigation).WithMany(p => p.Phones)
                .HasForeignKey(d => d.Brand)
                .HasConstraintName("phone_brand_fkey");

            entity.HasOne(d => d.ImageCodeNavigation).WithMany(p => p.Phones)
                .HasForeignKey(d => d.ImageCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("phone_image_code_fkey");
        });

        modelBuilder.Entity<PhoneHistory>(entity =>
        {
            entity.HasKey(e => new { e.Imei, e.Date, e.UserCode }).HasName("phone_history_pkey");

            entity.ToTable("phone_history");

            entity.Property(e => e.Imei)
                .HasColumnType("character varying")
                .HasColumnName("imei");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.UserCode)
                .HasColumnType("character varying")
                .HasColumnName("user_code");
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
                .HasConstraintName("phone_history_imei_fkey");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.PhoneHistories)
                .HasForeignKey(d => d.SaleCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("phone_history_sale_code_fkey");

            entity.HasOne(d => d.ToBranchNavigation).WithMany(p => p.PhoneHistories)
                .HasForeignKey(d => d.ToBranch)
                .HasConstraintName("phone_history_to_branch_fkey");

            entity.HasOne(d => d.UserCodeNavigation).WithMany(p => p.PhoneHistories)
                .HasForeignKey(d => d.UserCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("phone_history_user_code_fkey");
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
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.BrandNavigation).WithMany(p => p.PhoneRepairs)
                .HasForeignKey(d => d.Brand)
                .HasConstraintName("phone_repair_brand_fkey");

            entity.HasOne(d => d.ImageCodeNavigation).WithMany(p => p.PhoneRepairs)
                .HasForeignKey(d => d.ImageCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("phone_repair_image_code_fkey");
        });

        modelBuilder.Entity<PhoneRepairHistory>(entity =>
        {
            entity.HasKey(e => new { e.Imei, e.Date, e.UserCode }).HasName("phone_repair_history_pkey");

            entity.ToTable("phone_repair_history");

            entity.Property(e => e.Imei)
                .HasColumnType("character varying")
                .HasColumnName("imei");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.UserCode)
                .HasColumnType("character varying")
                .HasColumnName("user_code");
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
                .HasConstraintName("phone_repair_history_imei_fkey");

            entity.HasOne(d => d.SaleCodeNavigation).WithMany(p => p.PhoneRepairHistories)
                .HasForeignKey(d => d.SaleCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("phone_repair_history_sale_code_fkey");

            entity.HasOne(d => d.ToBranchNavigation).WithMany(p => p.PhoneRepairHistories)
                .HasForeignKey(d => d.ToBranch)
                .HasConstraintName("phone_repair_history_to_branch_fkey");

            entity.HasOne(d => d.UserCodeNavigation).WithMany(p => p.PhoneRepairHistories)
                .HasForeignKey(d => d.UserCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("phone_repair_history_user_code_fkey");
        });

        modelBuilder.Entity<PhoneRepairView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("phone_repair_view");

            entity.Property(e => e.ActionDescription).HasColumnName("action_description");
            entity.Property(e => e.Code)
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.CurrentState).HasColumnName("current_state");
            entity.Property(e => e.CustomerId)
                .HasColumnType("character varying")
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasColumnType("character varying")
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerNumber)
                .HasColumnType("character varying")
                .HasColumnName("customer_number");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");
        });

        modelBuilder.Entity<PhoneView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("phone_view");

            entity.Property(e => e.ActionDescription).HasColumnName("action_description");
            entity.Property(e => e.Code)
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.CurrentState).HasColumnName("current_state");
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");
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
            entity.Property(e => e.Warranty).HasColumnName("warranty");

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
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.Warranty).HasColumnName("warranty");
            entity.Property(e => e.Width).HasColumnName("width");

            entity.HasOne(d => d.BrandNavigation).WithMany(p => p.Screens)
                .HasForeignKey(d => d.Brand)
                .HasConstraintName("screen_brand_fkey");

            entity.HasOne(d => d.ImageCodeNavigation).WithMany(p => p.Screens)
                .HasForeignKey(d => d.ImageCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("screen_image_code_fkey");
        });

        modelBuilder.Entity<ScreenHistory>(entity =>
        {
            entity.HasKey(e => new { e.ScreenCode, e.Date, e.UserCode }).HasName("screen_history_pkey");

            entity.ToTable("screen_history");

            entity.Property(e => e.ScreenCode)
                .HasColumnType("character varying")
                .HasColumnName("screen_code");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.UserCode)
                .HasColumnType("character varying")
                .HasColumnName("user_code");
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("screen_history_sale_code_fkey");

            entity.HasOne(d => d.ScreenCodeNavigation).WithMany(p => p.ScreenHistories)
                .HasForeignKey(d => d.ScreenCode)
                .HasConstraintName("screen_history_screen_code_fkey");

            entity.HasOne(d => d.ToBranchNavigation).WithMany(p => p.ScreenHistories)
                .HasForeignKey(d => d.ToBranch)
                .HasConstraintName("screen_history_to_branch_fkey");

            entity.HasOne(d => d.UserCodeNavigation).WithMany(p => p.ScreenHistories)
                .HasForeignKey(d => d.UserCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("screen_history_user_code_fkey");
        });

        modelBuilder.Entity<ScreenView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("screen_view");

            entity.Property(e => e.Available).HasColumnName("available");
            entity.Property(e => e.Code)
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.Warranty).HasColumnName("warranty");
        });

        modelBuilder.Entity<Search>(entity =>
        {
            entity.HasKey(e => e.Date).HasName("searchs_pkey");

            entity.ToTable("searchs");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Value)
                .HasColumnType("character varying")
                .HasColumnName("value");
        });

        modelBuilder.Entity<Usd>(entity =>
        {
            entity.HasKey(e => e.Date).HasName("usd_pkey");

            entity.ToTable("usd");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Value).HasColumnName("value");
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

            entity.HasOne(d => d.UserCodeNavigation).WithOne(p => p.UserAccount)
                .HasForeignKey<UserAccount>(d => d.UserCode)
                .HasConstraintName("user_account_user_code_fkey");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserCode).HasName("user_info_pkey");

            entity.ToTable("user_info");

            entity.Property(e => e.UserCode)
                .HasColumnType("character varying")
                .HasColumnName("user_code");
            entity.Property(e => e.Branch)
                .HasColumnType("character varying")
                .HasColumnName("branch");
            entity.Property(e => e.ImageCode)
                .HasColumnType("character varying")
                .HasColumnName("image_code");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Role)
                .HasColumnType("character varying")
                .HasColumnName("role");

            entity.HasOne(d => d.BranchNavigation).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.Branch)
                .HasConstraintName("user_info_branch_fkey");

            entity.HasOne(d => d.ImageCodeNavigation).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.ImageCode)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_info_image_code_fkey");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.UserInfos)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("user_info_role_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
