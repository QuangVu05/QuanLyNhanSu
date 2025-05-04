using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class update1sobangnhu_gioitinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenTTLamViec",
                table: "TTLamViec");

            migrationBuilder.RenameColumn(
                name: "TenTTHonNhan",
                table: "TTHonNhan",
                newName: "TTHonNhans");

            migrationBuilder.RenameColumn(
                name: "GioiTinh",
                table: "NhanVien",
                newName: "IdGioiTinh");

            migrationBuilder.RenameColumn(
                name: "TenLoaiNV",
                table: "LoaiNhanVien",
                newName: "LoaiNV");

            migrationBuilder.AddColumn<string>(
                name: "TTLamViecs",
                table: "TTLamViec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GioiTinhIdGioiTinh",
                table: "NhanVien",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GioiTinh",
                columns: table => new
                {
                    IdGioiTinh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GioiTinhs = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioiTinh", x => x.IdGioiTinh);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_GioiTinh_GioiTinhIdGioiTinh",
                table: "NhanVien");

            migrationBuilder.DropTable(
                name: "GioiTinh");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_GioiTinhIdGioiTinh",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "TTLamViecs",
                table: "TTLamViec");

            migrationBuilder.DropColumn(
                name: "GioiTinhIdGioiTinh",
                table: "NhanVien");

            migrationBuilder.RenameColumn(
                name: "TTHonNhans",
                table: "TTHonNhan",
                newName: "TenTTHonNhan");

            migrationBuilder.RenameColumn(
                name: "IdGioiTinh",
                table: "NhanVien",
                newName: "GioiTinh");

            migrationBuilder.RenameColumn(
                name: "LoaiNV",
                table: "LoaiNhanVien",
                newName: "TenLoaiNV");

            migrationBuilder.AddColumn<string>(
                name: "TenTTLamViec",
                table: "TTLamViec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
