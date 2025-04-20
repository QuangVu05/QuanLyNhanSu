using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class addTienLuong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TienLuong",
                columns: table => new
                {
                    IdTL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNV = table.Column<int>(type: "int", nullable: false),
                    IdHD = table.Column<int>(type: "int", nullable: false),
                    HopDong = table.Column<int>(type: "int", nullable: true),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    SoNgayCong = table.Column<int>(type: "int", nullable: false),
                    LuongTangCa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LuongDaUng = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TienLuong", x => x.IdTL);
                    table.ForeignKey(
                        name: "FK_TienLuong_HopDongLaoDong_HopDong",
                        column: x => x.HopDong,
                        principalTable: "HopDongLaoDong",
                        principalColumn: "IdHD");
                    table.ForeignKey(
                        name: "FK_TienLuong_NhanVien_IdNV",
                        column: x => x.IdNV,
                        principalTable: "NhanVien",
                        principalColumn: "IdNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TienLuong_HopDong",
                table: "TienLuong",
                column: "HopDong");

            migrationBuilder.CreateIndex(
                name: "IX_TienLuong_IdNV",
                table: "TienLuong",
                column: "IdNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TienLuong");
        }
    }
}
