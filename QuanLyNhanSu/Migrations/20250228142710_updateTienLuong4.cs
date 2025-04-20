using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    /// <inheritdoc />
    public partial class updateTienLuong4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Tạo lại index và foreign key mới, sử dụng ON DELETE NO ACTION để tránh vấn đề cascade
            migrationBuilder.CreateIndex(
                name: "IX_TienLuong_IdHD",
                table: "TienLuong",
                column: "IdHD");

            migrationBuilder.AddForeignKey(
                name: "FK_TienLuong_HopDongLaoDong_IdHD",
                table: "TienLuong",
                column: "IdHD",
                principalTable: "HopDongLaoDong",
                principalColumn: "IdHD",
                onDelete: ReferentialAction.NoAction); // Sử dụng NoAction thay vì Cascade để tránh chu kỳ hoặc nhiều đường dẫn cascade
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Khôi phục lại nếu cần thiết (rollback các thay đổi nếu có)
            migrationBuilder.DropForeignKey(
                name: "FK_TienLuong_HopDongLaoDong_IdHD",
                table: "TienLuong");

            migrationBuilder.DropIndex(
                name: "IX_TienLuong_IdHD",
                table: "TienLuong");

            // Thêm lại cột "HopDongLaoDongIdHD" nếu muốn rollback
            migrationBuilder.AddColumn<int>(
                name: "HopDongLaoDongIdHD",
                table: "TienLuong",
                type: "int",
                nullable: true);

            // Tạo lại Index cho cột "HopDongLaoDongIdHD" nếu muốn rollback
            migrationBuilder.CreateIndex(
                name: "IX_TienLuong_HopDongLaoDongIdHD",
                table: "TienLuong",
                column: "HopDongLaoDongIdHD");

            migrationBuilder.AddForeignKey(
                name: "FK_TienLuong_HopDongLaoDong_HopDongLaoDongIdHD",
                table: "TienLuong",
                column: "HopDongLaoDongIdHD",
                principalTable: "HopDongLaoDong",
                principalColumn: "IdHD");
        }
    }
    
}
