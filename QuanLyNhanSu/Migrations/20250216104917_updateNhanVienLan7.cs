using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class updateNhanVienLan7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_LoaiNhanVien_IdLoaiNV",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien");

            migrationBuilder.AlterColumn<int>(
                name: "IdTTHonNhan",
                table: "NhanVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdTG",
                table: "NhanVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdTD",
                table: "NhanVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien",
                column: "IdTTHonNhan",
                principalTable: "TTHonNhan",
                principalColumn: "IdTTHonNhan");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien",
                column: "IdTG",
                principalTable: "TonGiao",
                principalColumn: "IdTG");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien",
                column: "IdTD",
                principalTable: "TrinhDo",
                principalColumn: "IdTD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_LoaiNhanVien_IdLoaiNV",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien");

            migrationBuilder.AlterColumn<int>(
                name: "IdTTHonNhan",
                table: "NhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdTG",
                table: "NhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdTD",
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
                name: "FK_NhanVien_TTHonNhan_IdTTHonNhan",
                table: "NhanVien",
                column: "IdTTHonNhan",
                principalTable: "TTHonNhan",
                principalColumn: "IdTTHonNhan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien",
                column: "IdTG",
                principalTable: "TonGiao",
                principalColumn: "IdTG",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TrinhDo_IdTD",
                table: "NhanVien",
                column: "IdTD",
                principalTable: "TrinhDo",
                principalColumn: "IdTD",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
