using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class AddKeyNhanVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    IdCV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCV = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVu", x => x.IdCV);
                });

            migrationBuilder.CreateTable(
                name: "DanToc",
                columns: table => new
                {
                    IdDT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanToc", x => x.IdDT);
                });

            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    IdPB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPB = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan", x => x.IdPB);
                });

            migrationBuilder.CreateTable(
                name: "TonGiao",
                columns: table => new
                {
                    IdTG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTG = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TonGiao", x => x.IdTG);
                });

            migrationBuilder.CreateTable(
                name: "TrinhDo",
                columns: table => new
                {
                    IdTD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrinhDo", x => x.IdTD);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    IdNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuocTich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiSinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TamChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiNV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TTHonNhan = table.Column<int>(type: "int", nullable: false),
                    TTLamViec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdBP = table.Column<int>(type: "int", nullable: false),
                    IdDT = table.Column<int>(type: "int", nullable: false),
                    IdPB = table.Column<int>(type: "int", nullable: false),
                    IdCV = table.Column<int>(type: "int", nullable: false),
                    IdTG = table.Column<int>(type: "int", nullable: false),
                    IdTD = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.IdNV);
                    table.ForeignKey(
                        name: "FK_NhanVien_BoPhan_IdBP",
                        column: x => x.IdBP,
                        principalTable: "BoPhan",
                        principalColumn: "IdBP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanVien_ChucVu_IdCV",
                        column: x => x.IdCV,
                        principalTable: "ChucVu",
                        principalColumn: "IdCV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanVien_DanToc_IdDT",
                        column: x => x.IdDT,
                        principalTable: "DanToc",
                        principalColumn: "IdDT",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanVien_PhongBan_IdPB",
                        column: x => x.IdPB,
                        principalTable: "PhongBan",
                        principalColumn: "IdPB",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanVien_TonGiao_IdTG",
                        column: x => x.IdTG,
                        principalTable: "TonGiao",
                        principalColumn: "IdTG",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanVien_TrinhDo_IdTD",
                        column: x => x.IdTD,
                        principalTable: "TrinhDo",
                        principalColumn: "IdTD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdBP",
                table: "NhanVien",
                column: "IdBP");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdCV",
                table: "NhanVien",
                column: "IdCV");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdDT",
                table: "NhanVien",
                column: "IdDT");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdPB",
                table: "NhanVien",
                column: "IdPB");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdTD",
                table: "NhanVien",
                column: "IdTD");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdTG",
                table: "NhanVien",
                column: "IdTG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "ChucVu");

            migrationBuilder.DropTable(
                name: "DanToc");

            migrationBuilder.DropTable(
                name: "PhongBan");

            migrationBuilder.DropTable(
                name: "TonGiao");

            migrationBuilder.DropTable(
                name: "TrinhDo");
        }
    }
}
