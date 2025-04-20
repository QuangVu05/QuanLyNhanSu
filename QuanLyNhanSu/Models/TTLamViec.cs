using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class TTLamViec
    {
        [Key] 
        public int IdTTLamViec { get; set; }
        [StringLength(50)]
        public string? TTLamViecs { get; set; }
        // Quan hệ 1-N với bảng NhanVien
        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
