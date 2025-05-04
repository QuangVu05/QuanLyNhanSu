using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class BoPhan
    {
        [Key]
        public int IdBP { get; set; }
        
        [Required(ErrorMessage = "Tên bộ phận là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên bộ phận không được vượt quá 50 ký tự.")]
      
        public string? TenBP { get; set; }
        // Quan hệ 1-N với bảng NhanVien
        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
