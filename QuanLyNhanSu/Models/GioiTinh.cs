using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class GioiTinh
    {
        [Key]
        public int IdGioiTinh { get; set; }
        [StringLength(10)] 
        
        public string GioiTinhs { get; set; }
        // Quan hệ 1-N với bảng NhanVien
        public virtual ICollection<NhanVien>? NhanViens { get; set; }
    }
}
