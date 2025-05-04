using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class TrinhDo
    {
        [Key]
        public int IdTD { get; set; }
        [Required(ErrorMessage = "Tên trình độ là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên trình độ không được vượt quá 50 ký tự.")]
        public string? TenTD { get; set; }
        // Quan hệ 1-N với bảng NhanVien
        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
