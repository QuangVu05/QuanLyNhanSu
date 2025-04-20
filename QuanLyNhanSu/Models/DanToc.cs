using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class DanToc
    {
        [Key]
        public int IdDT { get; set; }
        [Required(ErrorMessage = "Tên dân tộc là bắt buộc.")]
        [StringLength(15, ErrorMessage = "Tên bộ  không được vượt quá 15 ký tự.")]
        public string? TenDT { get; set; }
        // Quan hệ 1-N với bảng NhanVien
        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
