using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class updateLkNhanVien_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NhanVien_IdUser",
                table: "NhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdUser",
                table: "NhanVien",
                column: "IdUser",
                unique: true,
                filter: "[IdUser] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NhanVien_IdUser",
                table: "NhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdUser",
                table: "NhanVien",
                column: "IdUser");
        }
    }
}
