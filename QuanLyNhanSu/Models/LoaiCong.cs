using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Models
{
    public class LoaiCong
    {
        [Key]
        
        public int IdCong { get; set; }

        [Required(ErrorMessage = "Tên loại công không được để trống.")]
        [StringLength(40, ErrorMessage = "Tên loại công không được vượt quá 40 ký tự.")]
        public string? TenCong { get; set; }

        [Required(ErrorMessage = "Hệ số lương không được để trống.")]
        [Range(0.1, 10.0, ErrorMessage = "Hệ số lương phải nằm trong khoảng từ 0.1 đến 10.0.")]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? HeSoLuong { get; set; }

       
    }
}
