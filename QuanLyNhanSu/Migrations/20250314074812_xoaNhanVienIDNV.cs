using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class xoaNhanVienIDNV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Xóa khóa ngoại trước
            migrationBuilder.DropForeignKey(
                name: "FK_KhenThuong_KyLuat_NhanVien_NhanVienIdNV",
                table: "KhenThuong_KyLuat");

            // Xóa Index trước
            migrationBuilder.DropIndex(
                name: "IX_KhenThuong_KyLuat_NhanVienIdNV",
                table: "KhenThuong_KyLuat");

            // Xóa cột
            migrationBuilder.DropColumn(
                name: "NhanVienIdNV",
                table: "KhenThuong_KyLuat");

            migrationBuilder.AlterColumn<string>(
                name: "SoKTKL",
                table: "KhenThuong_KyLuat",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
            name: "NhanVienIdNV",
            table: "KhenThuong_KyLuat",
            type: "int",
            nullable: false,
            defaultValue: 0);

            // Tạo lại Index
            migrationBuilder.CreateIndex(
                name: "IX_KhenThuong_KyLuat_NhanVienIdNV",
                table: "KhenThuong_KyLuat",
                column: "NhanVienIdNV");

            // Tạo lại khóa ngoại
            migrationBuilder.AddForeignKey(
                name: "FK_KhenThuong_KyLuat_NhanVien_NhanVienIdNV",
                table: "KhenThuong_KyLuat",
                column: "NhanVienIdNV",
                principalTable: "NhanVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AlterColumn<string>(
                name: "SoKTKL",
                table: "KhenThuong_KyLuat",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
