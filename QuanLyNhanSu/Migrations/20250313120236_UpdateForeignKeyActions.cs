using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKeyActions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Cập nhật các foreign key để SET NULL nếu cần thiết
           

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien",
                column: "IdTG",
                principalTable: "TonGiao",
                principalColumn: "IdTG",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien",
                column: "IdTD",
                principalTable: "TrinhDo",
                principalColumn: "IdTD",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien",
                column: "IdTTHonNhan",
                principalTable: "TTHonNhan",
                principalColumn: "IdTTHonNhan",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TTLamViec_IdTTLamViec",
                table: "NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TTLamViec_IdTTLamViec",
                table: "NhanVien",
                column: "IdTTLamViec",
                principalTable: "TTLamViec",
                principalColumn: "IdTTLamViec",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Hủy bỏ các thay đổi và quay lại trạng thái ban đầu
            

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien",
                column: "IdTG",
                principalTable: "TonGiao",
                principalColumn: "IdTG",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien",
                column: "IdTD",
                principalTable: "TrinhDo",
                principalColumn: "IdTD",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien",
                column: "IdTTHonNhan",
                principalTable: "TTHonNhan",
                principalColumn: "IdTTHonNhan",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TTLamViec_IdTTLamViec",
                table: "NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TTLamViec_IdTTLamViec",
                table: "NhanVien",
                column: "IdTTLamViec",
                principalTable: "TTLamViec",
                principalColumn: "IdTTLamViec",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
