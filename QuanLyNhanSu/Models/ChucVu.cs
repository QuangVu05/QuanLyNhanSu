using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class ChucVu
    {
        [Key]
        public int IdCV { get; set; }
        [Required(ErrorMessage = "Tên chức vụ là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên chức vụ không được vượt quá 50 ký tự.")]
        public string? TenCV { get; set; }
        // Quan hệ 1-N với bảng NhanVien
        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
