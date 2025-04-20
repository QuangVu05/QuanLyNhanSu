using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class updateNhanVienlan2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "LoaiNV",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "TTLamViec",
                table: "NhanVien");

            migrationBuilder.RenameColumn(
                name: "TTHonNhan",
                table: "NhanVien",
                newName: "IdTTLamViec");

            migrationBuilder.AlterColumn<int>(
                name: "IdTG",
                table: "NhanVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdLoaiNV",
                table: "NhanVien",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTTHonNhan",
                table: "NhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LoaiNhanVien",
                columns: table => new
                {
                    IdLoaiNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiNV = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiNhanVien", x => x.IdLoaiNV);
                });

            migrationBuilder.CreateTable(
                name: "TTHonNhan",
                columns: table => new
                {
                    IdTTHonNhan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTTHonNhan = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTHonNhan", x => x.IdTTHonNhan);
                });

            migrationBuilder.CreateTable(
                name: "TTLamViec",
                columns: table => new
                {
                    IdTTLamViec = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTTLamViec = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTLamViec", x => x.IdTTLamViec);
                });
            // Thêm dữ liệu mẫu vào các bảng
            migrationBuilder.Sql("INSERT INTO LoaiNhanVien (TenLoaiNV) VALUES ('Nhân viên chính thức')");
            migrationBuilder.Sql("INSERT INTO LoaiNhanVien (TenLoaiNV) VALUES ('Nhân viên hợp đồng')");
            migrationBuilder.Sql("INSERT INTO LoaiNhanVien (TenLoaiNV) VALUES ('Nhân viên thời vụ')");

            migrationBuilder.Sql("INSERT INTO TTHonNhan (TenTTHonNhan) VALUES ('Độc thân')");
            migrationBuilder.Sql("INSERT INTO TTHonNhan (TenTTHonNhan) VALUES ('Đã kết hôn')");
            migrationBuilder.Sql("INSERT INTO TTHonNhan (TenTTHonNhan) VALUES ('Li hôn')");

            migrationBuilder.Sql("INSERT INTO TTLamViec (TenTTLamViec) VALUES ('Đang làm việc')");
            migrationBuilder.Sql("INSERT INTO TTLamViec (TenTTLamViec) VALUES ('Đã nghỉ việc')");
            migrationBuilder.Sql("INSERT INTO TTLamViec (TenTTLamViec) VALUES ('Tạm ngừng công việc')");
            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdLoaiNV",
                table: "NhanVien",
                column: "IdLoaiNV");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdTTHonNhan",
                table: "NhanVien",
                column: "IdTTHonNhan");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdTTLamViec",
                table: "NhanVien",
                column: "IdTTLamViec");

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
                principalColumn: "IdTTHonNhan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TTLamViec_IdTTLamViec",
                table: "NhanVien",
                column: "IdTTLamViec",
                principalTable: "TTLamViec",
                principalColumn: "IdTTLamViec",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien",
                column: "IdTG",
                principalTable: "TonGiao",
                principalColumn: "IdTG");
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
                name: "FK_NhanVien_TTLamViec_IdTTLamViec",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien");

            migrationBuilder.DropTable(
                name: "LoaiNhanVien");

            migrationBuilder.DropTable(
                name: "TTHonNhan");

            migrationBuilder.DropTable(
                name: "TTLamViec");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_IdLoaiNV",
                table: "NhanVien");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_IdTTHonNhan",
                table: "NhanVien");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_IdTTLamViec",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "IdLoaiNV",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "IdTTHonNhan",
                table: "NhanVien");

            migrationBuilder.RenameColumn(
                name: "IdTTLamViec",
                table: "NhanVien",
                newName: "TTHonNhan");

            migrationBuilder.AlterColumn<int>(
                name: "IdTG",
                table: "NhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoaiNV",
                table: "NhanVien",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TTLamViec",
                table: "NhanVien",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TonGiao_IdTG",
                table: "NhanVien",
                column: "IdTG",
                principalTable: "TonGiao",
                principalColumn: "IdTG",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
