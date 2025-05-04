using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class updateNhanVienLan3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien");

            migrationBuilder.AlterColumn<string>(
                name: "QuocTich",
                table: "NhanVien",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "NhanVien",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdTTHonNhan",
                table: "NhanVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdTD",
                table: "NhanVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien",
                column: "IdTTHonNhan",
                principalTable: "TTHonNhan",
                principalColumn: "IdTTHonNhan");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien",
                column: "IdTD",
                principalTable: "TrinhDo",
                principalColumn: "IdTD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien");

            migrationBuilder.AlterColumn<string>(
                name: "QuocTich",
                table: "NhanVien",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "NhanVien",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "IdTTHonNhan",
                table: "NhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdTD",
                table: "NhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien",
                column: "IdTTHonNhan",
                principalTable: "TTHonNhan",
                principalColumn: "IdTTHonNhan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien",
                column: "IdTD",
                principalTable: "TrinhDo",
                principalColumn: "IdTD",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
