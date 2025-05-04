using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class TTHonNhan
    {
        [Key]
        public int IdTTHonNhan { get; set; }
        [StringLength(25)]
        public string TTHonNhans { get; set; }
        // Quan hệ 1-N với bảng NhanVien
        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
