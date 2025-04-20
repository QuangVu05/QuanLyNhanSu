using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class PhongBan
    {
        [Key]
        public int IdPB { get; set; }
        [Required(ErrorMessage = "Tên phòng ban là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên phòng ban không được vượt quá 50 ký tự.")]
        public string? TenPB { get; set; }
        // Quan hệ 1-N với bảng NhanVien
        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
