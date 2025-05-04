
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhanSu.Models
{
   
    public class HopDongLaoDong
    {
        [Key]
        public int IdHD { get; set; }

        [Required(ErrorMessage = "Số hợp đồng không được để trống.")]
        [StringLength(50, ErrorMessage = "Mã hợp đồng không được quá 50 ký tự.")]
        public string SoHD { get; set; }

        [Required(ErrorMessage = "Nhân viên không được để trống.")]
        [ForeignKey("NhanVien")]
        public int? IdNV { get; set; }
        public virtual NhanVien? NhanVien { get; set; }
        [Required(ErrorMessage = "Ngày ký không được để trống.")]
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayKy { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayBatDau { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayKetThuc { get; set; }


        
        [StringLength(50, ErrorMessage = "Loại hợp đồng không được quá 50 ký tự.")]
        public string? LoaiHopDong { get; set; }

        

        [Required(ErrorMessage = "Lương cơ bản không được để trống.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(3450000, 1000000000, ErrorMessage = "Lương cơ bản phải từ 3.450.000 triệu đến 1 tỉ.")]
        
        public decimal? LuongCoBan { get; set; }
        [Column(TypeName = "decimal(4,2)")]
        [Range(0.5, 10, ErrorMessage = "Hệ số lương phải từ 0.5 đến 10.")]
        [Required(ErrorMessage = "Hệ số lương không được để trống.")]
        public decimal? HeSoLuong { get; set; }
   
        public string? NoiDung { get; set; }
        public virtual ICollection<TienLuong>? TienLuongs { get; set; } // Quan hệ 1-N


    }
}
