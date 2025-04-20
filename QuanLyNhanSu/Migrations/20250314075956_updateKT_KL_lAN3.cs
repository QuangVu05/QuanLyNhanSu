using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class updateKT_KL_lAN3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddForeignKey(
                name: "FK_KhenThuong_KyLuat_NhanVien_IdNV",
                table: "KhenThuong_KyLuat",
                column: "IdNV",
                principalTable: "NhanVien",
                principalColumn: "IdNV",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddForeignKey(
                name: "FK_KhenThuong_KyLuat_NhanVien_NhanVienIdNV",
                table: "KhenThuong_KyLuat",
                column: "NhanVienIdNV",
                principalTable: "NhanVien",
                principalColumn: "IdNV");
        }
    }
}
