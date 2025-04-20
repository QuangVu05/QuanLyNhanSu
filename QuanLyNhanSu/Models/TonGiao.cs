using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class TonGiao
    {
        [Key]
        public int IdTG { get; set; }
        [Required(ErrorMessage = "Tên tôn giáo là bắt buộc.")]
        [StringLength(55, ErrorMessage = "Tên tôn giáo không được vượt quá 55 ký tự.")]
        public string? TenTG  { get; set; }
        // Quan hệ 1-N với bảng NhanVien
        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
