using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhanSu.Models
{
    public class TienLuong
    {
        [Key]
        public int IdTL { get; set; }

        [Required(ErrorMessage = "Nhân viên không được để trống!")]
        [ForeignKey("NhanVien")]
        public int IdNV { get; set; }
        public virtual NhanVien? NhanVien { get; set; }
  

        [ForeignKey("HopDongLaoDong")]
        public int IdHD { get; set; }
        public virtual HopDongLaoDong? HopDongLaoDong { get; set; } // Lấy lương tháng & hệ số lương từ hợp đồng

        
        [Required(ErrorMessage = "Tháng không được để trống!")]
        [Range(1, 12, ErrorMessage = "Tháng phải từ 1 đến 12!")]
        public int Thang { get; set; }

        [Required(ErrorMessage = "Năm không được để trống!")]
        [Range(2000, 2100, ErrorMessage = "Năm phải nằm trong khoảng 2000 - 2100!")]
        public int Nam { get; set; }


        [Required(ErrorMessage = "Số ngày công không được để trống!")]
        [Range(0, 40, ErrorMessage = "Số ngày công phải từ 0 đến 40!")]
        public int SoNgayCong { get; set; } // Số ngày làm việc thực tế

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Lương tăng ca không thể là số âm!")]
        public decimal? LuongTangCa { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Lương đã ứng không thể là số âm!")]
        public decimal? LuongDaUng { get; set; } = 0;


        [Column(TypeName = "decimal(18,2)")]
        public decimal TongLuong { get; set; }
        public bool DaNhanTien { get; set; } = false;
        [Column(TypeName = "date")]  // Chỉ lưu ngày, tháng, năm
        public DateTime? NgayNhanTien { get; set; }
    }
}
