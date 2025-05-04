namespace QuanLyNhanSu.ViewModels
{
    public class NhanVienViewModel
    {
        public int IdNV { get; set; }
        public string? MaNV { get; set; }
        public string? Anh { get; set; }
        public string? TenNV { get; set; }
        public string? SDT { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? QuocTich { get; set; }

        public string? TenPB { get; set; }
        public string? TenCV { get; set; }
        public string? TTLamViecs { get; set; }

        // Phương thức này để lấy lớp CSS theo trạng thái
        public string GetStatusClass(string status)
        {
            switch (status)
            {
                case "Đang làm việc":
                    return "bg-success"; // Màu xanh lá cho "Đang làm việc"
                case "Nghỉ phép":
                    return "bg-warning"; // Màu vàng cho "Nghỉ phép"
                case "Đã nghỉ việc":
                    return "bg-danger"; // Màu đỏ cho "Đã nghỉ việc"
                default:
                    return "bg-secondary"; // Màu xám cho trạng thái mặc định
            }
        }
    }
}
