using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class addKT_KL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhenThuong_KyLuat",
                columns: table => new
                {
                    SoKTKL = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IdNV = table.Column<int>(type: "int", nullable: false),
                    NhanVienIdNV = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhenThuong_KyLuat", x => x.SoKTKL);
                    table.ForeignKey(
                        name: "FK_KhenThuong_KyLuat_NhanVien_NhanVienIdNV",
                        column: x => x.NhanVienIdNV,
                        principalTable: "NhanVien",
                        principalColumn: "IdNV");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KhenThuong_KyLuat_NhanVienIdNV",
                table: "KhenThuong_KyLuat",
                column: "NhanVienIdNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhenThuong_KyLuat");
        }
    }
}
