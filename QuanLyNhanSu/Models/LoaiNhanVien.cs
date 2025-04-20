using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class LoaiNhanVien
    {
        [Key]
        public int IdLoaiNV { get; set; }
        [StringLength(50)]
        public string LoaiNV { get; set; }
        // Quan hệ 1-N với bảng NhanVien
        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
