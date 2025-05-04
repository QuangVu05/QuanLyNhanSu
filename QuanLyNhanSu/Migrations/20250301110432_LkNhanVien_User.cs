using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class LkNhanVien_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "NhanVien",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdUser",
                table: "NhanVien",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_AspNetUsers_IdUser",
                table: "NhanVien",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_AspNetUsers_IdUser",
                table: "NhanVien");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_IdUser",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "NhanVien");
        }
    }
}
