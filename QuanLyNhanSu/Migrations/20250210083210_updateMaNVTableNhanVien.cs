using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class updateMaNVTableNhanVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm cột MaNV vào bảng NhanVien
            migrationBuilder.AddColumn<string>(
                name: "MaNV",
                table: "NhanVien",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);  // Nếu bạn muốn cột này có thể NULL
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa cột MaNV nếu rollback migration
            migrationBuilder.DropColumn(
                name: "MaNV",
                table: "NhanVien");
        }
    }
}
