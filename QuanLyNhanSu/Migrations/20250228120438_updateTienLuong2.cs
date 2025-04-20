using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class updateTienLuong2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TienLuong_HopDongLaoDong_HopDong",
                table: "TienLuong");

            migrationBuilder.RenameColumn(
                name: "HopDong",
                table: "TienLuong",
                newName: "HopDongLaoDongIdHD");

            migrationBuilder.RenameIndex(
                name: "IX_TienLuong_HopDong",
                table: "TienLuong",
                newName: "IX_TienLuong_HopDongLaoDongIdHD");

            migrationBuilder.AlterColumn<decimal>(
                name: "LuongTangCa",
                table: "TienLuong",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LuongDaUng",
                table: "TienLuong",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<bool>(
                name: "DaNhanTien",
                table: "TienLuong",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TienLuong_HopDongLaoDong_HopDongLaoDongIdHD",
                table: "TienLuong",
                column: "HopDongLaoDongIdHD",
                principalTable: "HopDongLaoDong",
                principalColumn: "IdHD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TienLuong_HopDongLaoDong_HopDongLaoDongIdHD",
                table: "TienLuong");

            migrationBuilder.DropColumn(
                name: "DaNhanTien",
                table: "TienLuong");

            migrationBuilder.RenameColumn(
                name: "HopDongLaoDongIdHD",
                table: "TienLuong",
                newName: "HopDong");

            migrationBuilder.RenameIndex(
                name: "IX_TienLuong_HopDongLaoDongIdHD",
                table: "TienLuong",
                newName: "IX_TienLuong_HopDong");

            migrationBuilder.AlterColumn<decimal>(
                name: "LuongTangCa",
                table: "TienLuong",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LuongDaUng",
                table: "TienLuong",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TienLuong_HopDongLaoDong_HopDong",
                table: "TienLuong",
                column: "HopDong",
                principalTable: "HopDongLaoDong",
                principalColumn: "IdHD");
        }
    }
}
