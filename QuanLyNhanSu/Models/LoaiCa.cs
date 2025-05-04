using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhanSu.Models
{
    public class LoaiCa
    {
        [Key]
        public int IdCa { get; set; } // Mã loại ca làm việc (Primary Key)

        [Required(ErrorMessage = "Tên ca không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên ca không được quá 50 ký tự.")]
        public string TenCa { get; set; } // Tên ca làm việc (Sáng, Chiều, Tối,...)

        [Required(ErrorMessage = "Giờ bắt đầu không được để trống.")]
        public TimeSpan? GioBatDau { get; set; } // Giờ bắt đầu ca làm việc

        [Required(ErrorMessage = "Giờ kết thúc không được để trống.")]
        public TimeSpan? GioKetThuc { get; set; } // Giờ kết thúc ca làm việc

        [Required(ErrorMessage = "Hệ số lương không được để trống.")]
        [Range(0.5, 5.0, ErrorMessage = "Hệ số lương phải từ 0.5 đến 5.0.")]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? HeSoLuong { get; set; } // Hệ số lương của ca làm việc
    }
}
