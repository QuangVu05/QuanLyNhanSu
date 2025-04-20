namespace QuanLyNhanSu.ViewModels
{
    public class TongQuanViewModel
    {
        public int TongSoNhanVien { get; set; } 
        public int TongSoPhongBan { get; set; }
        public int TongSoHopDong { get; set; }
        public int TongSoTaiKhoan { get; set; }
        public int NhanVienDaNghiViec { get; set; }
        public int NhanVienDangLamViec { get; set; }
        public int NhanVienTamNghi { get; set; }

        // Thêm dữ liệu cho biểu đồ thống kê nhân viên theo phòng ban
        public List<string> DanhSachPhongBan { get; set; }
        public List<int> NhanVienDangLamTheoPhongBan { get; set; }
        public List<int> NhanVienTamNghiTheoPhongBan { get; set; }

        public TongQuanViewModel()
        {
            DanhSachPhongBan = new List<string>();
            NhanVienDangLamTheoPhongBan = new List<int>();
            NhanVienTamNghiTheoPhongBan = new List<int>();
        }
    }
}
