using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNhanVienLan8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_LoaiNhanVien_IdLoaiNV",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TTLamViec_IdTTLamViec",
                table: "NhanVien");

            migrationBuilder.AlterColumn<int>(
                name: "IdTTLamViec",
                table: "NhanVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdLoaiNV",
                table: "NhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_LoaiNhanVien_IdLoaiNV",
                table: "NhanVien",
                column: "IdLoaiNV",
                principalTable: "LoaiNhanVien",
                principalColumn: "IdLoaiNV",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TTLamViec_IdTTLamViec",
                table: "NhanVien",
                column: "IdTTLamViec",
                principalTable: "TTLamViec",
                principalColumn: "IdTTLamViec");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_LoaiNhanVien_IdLoaiNV",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TTLamViec_IdTTLamViec",
                table: "NhanVien");

            migrationBuilder.AlterColumn<int>(
                name: "IdTTLamViec",
                table: "NhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdLoaiNV",
                table: "NhanVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_LoaiNhanVien_IdLoaiNV",
                table: "NhanVien",
                column: "IdLoaiNV",
                principalTable: "LoaiNhanVien",
                principalColumn: "IdLoaiNV");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TTLamViec_IdTTLamViec",
                table: "NhanVien",
                column: "IdTTLamViec",
                principalTable: "TTLamViec",
                principalColumn: "IdTTLamViec",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
