using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhanSu.Models
{
    public class KhenThuong_KyLuat
    {
       
        [Key]
        public int Id { get; set; }
       
        [StringLength(20, ErrorMessage = "Số KTKL không được vượt quá 20 ký tự.")]

        public  string? SoKTKL { get; set; }
        [Required(ErrorMessage = "Loại quyết định không được để trống.")]
        [StringLength(10, ErrorMessage = "Loại quyết định không được vượt quá 10 ký tự.")]
        public string? LoaiQuyetDinh { get; set; }

        [Required(ErrorMessage = "Ngày không được để trống.")]
        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Lý do không được để trống.")]
        [StringLength(100, ErrorMessage = "Lý do không được vượt quá 100 ký tự.")]
        public string? LyDo { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống.")]
        [StringLength(500, ErrorMessage = "Nội dung không được vượt quá 500 ký tự.")]
        public  string? NoiDung { get; set; }
        [ForeignKey("NhanVien")]
        // 🔗 Khóa ngoại liên kết với nhân viên
        [Required(ErrorMessage = "Nhân viên không được để trống.")]
        public int? IdNV { get; set; }
        // ⚡ Navigation Property để truy xuất thông tin nhân viên
        public  virtual NhanVien? NhanVien { get; set; }


    }
}
