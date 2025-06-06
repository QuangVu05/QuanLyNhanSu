﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyNhanSu.Data;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    [DbContext(typeof(QuanLyNhanSuContext))]
    [Migration("20250222100952_addLoaiCa3")]
    partial class addLoaiCa3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuanLyNhanSu.Models.BoPhan", b =>
                {
                    b.Property<int>("IdBP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBP"));

                    b.Property<string>("TenBP")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdBP");

                    b.ToTable("BoPhan");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.ChucVu", b =>
                {
                    b.Property<int>("IdCV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCV"));

                    b.Property<string>("TenCV")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdCV");

                    b.ToTable("ChucVu");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.DanToc", b =>
                {
                    b.Property<int>("IdDT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDT"));

                    b.Property<string>("TenDT")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("IdDT");

                    b.ToTable("DanToc");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.GioiTinh", b =>
                {
                    b.Property<int>("IdGioiTinh")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdGioiTinh"));

                    b.Property<string>("GioiTinhs")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("IdGioiTinh");

                    b.ToTable("GioiTinh");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.HopDongLaoDong", b =>
                {
                    b.Property<int>("IdHD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHD"));

                    b.Property<decimal?>("HeSoLuong")
                        .IsRequired()
                        .HasColumnType("decimal(4,2)");

                    b.Property<int?>("IdNV")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("LoaiHopDong")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("LuongCoBan")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("NgayBatDau")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKy")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoHD")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdHD");

                    b.HasIndex("IdNV");

                    b.ToTable("HopDongLaoDong");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.LoaiCa", b =>
                {
                    b.Property<int>("IdCa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCa"));

                    b.Property<TimeSpan>("GioBatDau")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("GioKetThuc")
                        .HasColumnType("time");

                    b.Property<decimal>("HeSoLuong")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TenCa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdCa");

                    b.ToTable("LoaiCa");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.LoaiNhanVien", b =>
                {
                    b.Property<int>("IdLoaiNV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLoaiNV"));

                    b.Property<string>("LoaiNV")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdLoaiNV");

                    b.ToTable("LoaiNhanVien");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.NhanVien", b =>
                {
                    b.Property<int>("IdNV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNV"));

                    b.Property<string>("Anh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CCCD")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("HoKhau")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("IdBP")
                        .HasColumnType("int");

                    b.Property<int>("IdCV")
                        .HasColumnType("int");

                    b.Property<int>("IdDT")
                        .HasColumnType("int");

                    b.Property<int>("IdGioiTinh")
                        .HasColumnType("int");

                    b.Property<int?>("IdLoaiNV")
                        .HasColumnType("int");

                    b.Property<int>("IdPB")
                        .HasColumnType("int");

                    b.Property<int?>("IdTD")
                        .HasColumnType("int");

                    b.Property<int?>("IdTG")
                        .HasColumnType("int");

                    b.Property<int?>("IdTTHonNhan")
                        .HasColumnType("int");

                    b.Property<int>("IdTTLamViec")
                        .HasColumnType("int");

                    b.Property<string>("MaNV")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("NgaySinh")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiSinh")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("QueQuan")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("QuocTich")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("TamChu")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("TenNV")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdNV");

                    b.HasIndex("IdBP");

                    b.HasIndex("IdCV");

                    b.HasIndex("IdDT");

                    b.HasIndex("IdGioiTinh");

                    b.HasIndex("IdLoaiNV");

                    b.HasIndex("IdPB");

                    b.HasIndex("IdTD");

                    b.HasIndex("IdTG");

                    b.HasIndex("IdTTHonNhan");

                    b.HasIndex("IdTTLamViec");

                    b.ToTable("NhanVien");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.PhongBan", b =>
                {
                    b.Property<int>("IdPB")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPB"));

                    b.Property<string>("TenPB")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdPB");

                    b.ToTable("PhongBan");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.TTHonNhan", b =>
                {
                    b.Property<int>("IdTTHonNhan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTTHonNhan"));

                    b.Property<string>("TTHonNhans")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("IdTTHonNhan");

                    b.ToTable("TTHonNhan");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.TTLamViec", b =>
                {
                    b.Property<int>("IdTTLamViec")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTTLamViec"));

                    b.Property<string>("TTLamViecs")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdTTLamViec");

                    b.ToTable("TTLamViec");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.TonGiao", b =>
                {
                    b.Property<int>("IdTG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTG"));

                    b.Property<string>("TenTG")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.HasKey("IdTG");

                    b.ToTable("TonGiao");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.TrinhDo", b =>
                {
                    b.Property<int>("IdTD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTD"));

                    b.Property<string>("TenTD")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdTD");

                    b.ToTable("TrinhDo");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.HopDongLaoDong", b =>
                {
                    b.HasOne("QuanLyNhanSu.Models.NhanVien", "NhanVien")
                        .WithMany("HopDongLaoDongs")
                        .HasForeignKey("IdNV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.NhanVien", b =>
                {
                    b.HasOne("QuanLyNhanSu.Models.BoPhan", "BoPhan")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdBP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyNhanSu.Models.ChucVu", "ChucVu")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdCV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyNhanSu.Models.DanToc", "DanToc")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdDT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyNhanSu.Models.GioiTinh", "GioiTinh")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdGioiTinh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyNhanSu.Models.LoaiNhanVien", "LoaiNhanVien")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdLoaiNV");

                    b.HasOne("QuanLyNhanSu.Models.PhongBan", "PhongBan")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdPB")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyNhanSu.Models.TrinhDo", "TrinhDo")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdTD");

                    b.HasOne("QuanLyNhanSu.Models.TonGiao", "TonGiao")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdTG");

                    b.HasOne("QuanLyNhanSu.Models.TTHonNhan", "TTHonNhan")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdTTHonNhan");

                    b.HasOne("QuanLyNhanSu.Models.TTLamViec", "TTLamViec")
                        .WithMany("NhanViens")
                        .HasForeignKey("IdTTLamViec")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoPhan");

                    b.Navigation("ChucVu");

                    b.Navigation("DanToc");

                    b.Navigation("GioiTinh");

                    b.Navigation("LoaiNhanVien");

                    b.Navigation("PhongBan");

                    b.Navigation("TTHonNhan");

                    b.Navigation("TTLamViec");

                    b.Navigation("TonGiao");

                    b.Navigation("TrinhDo");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.BoPhan", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.ChucVu", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.DanToc", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.GioiTinh", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.LoaiNhanVien", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.NhanVien", b =>
                {
                    b.Navigation("HopDongLaoDongs");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.PhongBan", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.TTHonNhan", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.TTLamViec", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.TonGiao", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.TrinhDo", b =>
                {
                    b.Navigation("NhanViens");
                });
#pragma warning restore 612, 618
        }
    }
}
