using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class updateKT_KL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KhenThuong_KyLuat",
                table: "KhenThuong_KyLuat");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ngay",
                table: "KhenThuong_KyLuat",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "KhenThuong_KyLuat",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhenThuong_KyLuat",
                table: "KhenThuong_KyLuat",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KhenThuong_KyLuat",
                table: "KhenThuong_KyLuat");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "KhenThuong_KyLuat");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ngay",
                table: "KhenThuong_KyLuat",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhenThuong_KyLuat",
                table: "KhenThuong_KyLuat",
                column: "SoKTKL");
        }
    }
}
