using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class udateFkdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Xóa các ràng buộc khóa ngoại cũ
            migrationBuilder.DropForeignKey("FK_NhanVien_LoaiNhanVien_IdLoaiNV", "NhanVien");
            migrationBuilder.DropForeignKey("FK_NhanVien_TTHonNhan_IdTTHonNhan", "NhanVien");
            migrationBuilder.DropForeignKey("FK_NhanVien_GioiTinh_IdGioiTinh", "NhanVien");
            migrationBuilder.DropForeignKey("FK_NhanVien_BoPhan_IdBP", "NhanVien");
            migrationBuilder.DropForeignKey("FK_NhanVien_ChucVu_IdCV", "NhanVien");
            migrationBuilder.DropForeignKey("FK_NhanVien_DanToc_IdDT", "NhanVien");
            migrationBuilder.DropForeignKey("FK_NhanVien_PhongBan_IdPB", "NhanVien");

            // Thêm lại các ràng buộc với ON DELETE NO ACTION
            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_LoaiNhanVien_IdLoaiNV",
                table: "NhanVien",
                column: "IdLoaiNV",
                principalTable: "LoaiNhanVien",
                principalColumn: "IdLoaiNV",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien",
                column: "IdTTHonNhan",
                principalTable: "TTHonNhan",
                principalColumn: "IdTTHonNhan",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_GioiTinh_IdGioiTinh",
                table: "NhanVien",
                column: "IdGioiTinh",
                principalTable: "GioiTinh",
                principalColumn: "IdGioiTinh",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_BoPhan_IdBP",
                table: "NhanVien",
                column: "IdBP",
                principalTable: "BoPhan",
                principalColumn: "IdBP",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_ChucVu_IdCV",
                table: "NhanVien",
                column: "IdCV",
                principalTable: "ChucVu",
                principalColumn: "IdCV",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_DanToc_IdDT",
                table: "NhanVien",
                column: "IdDT",
                principalTable: "DanToc",
                principalColumn: "IdDT",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_PhongBan_IdPB",
                table: "NhanVien",
                column: "IdPB",
                principalTable: "PhongBan",
                principalColumn: "IdPB",
                onDelete: ReferentialAction.NoAction);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
