using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class updateNhanVienLan4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_GioiTinh_GioiTinhIdGioiTinh",
                table: "NhanVien");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_GioiTinhIdGioiTinh",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "GioiTinhIdGioiTinh",
                table: "NhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdGioiTinh",
                table: "NhanVien",
                column: "IdGioiTinh");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_GioiTinh_IdGioiTinh",
                table: "NhanVien",
                column: "IdGioiTinh",
                principalTable: "GioiTinh",
                principalColumn: "IdGioiTinh",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_GioiTinh_IdGioiTinh",
                table: "NhanVien");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_IdGioiTinh",
                table: "NhanVien");

            migrationBuilder.AddColumn<int>(
                name: "GioiTinhIdGioiTinh",
                table: "NhanVien",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_GioiTinhIdGioiTinh",
                table: "NhanVien",
                column: "GioiTinhIdGioiTinh");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_GioiTinh_GioiTinhIdGioiTinh",
                table: "NhanVien",
                column: "GioiTinhIdGioiTinh",
                principalTable: "GioiTinh",
                principalColumn: "IdGioiTinh");
        }
    }
}
