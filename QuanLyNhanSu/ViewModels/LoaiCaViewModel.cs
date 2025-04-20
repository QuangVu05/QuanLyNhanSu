using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.ViewModels
{
    public class LoaiCaViewModel
    {
        public int? IdCa { get; set; } // Nullable để dùng khi tạo mới

        [Required(ErrorMessage = "Tên ca không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên ca không được quá 50 ký tự.")]
        public string? TenCa { get; set; } // Tên ca làm việc (Sáng, Chiều, Tối,...)

        [Required(ErrorMessage = "Giờ bắt đầu không được để trống.")]
        public TimeSpan? GioBatDau { get; set; } // Giờ bắt đầu ca làm việc

        [Required(ErrorMessage = "Giờ kết thúc không được để trống.")]
        public TimeSpan? GioKetThuc { get; set; } // Giờ kết thúc ca làm việc

        [Required(ErrorMessage = "Hệ số lương không được để trống.")]
        [Range(0.5, 5.0, ErrorMessage = "Hệ số lương phải từ 0.5 đến 5.0.")]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? HeSoLuong { get; set; } // Hệ số lương của ca làm việc
                                                // Tính tổng số giờ làm việc
        public double? TongGioLam
        {
            get
            {
                if (GioBatDau.HasValue && GioKetThuc.HasValue)
                {
                    if (GioKetThuc < GioBatDau) // Xử lý làm qua đêm
                    {
                        return (24 - GioBatDau.Value.TotalHours) + GioKetThuc.Value.TotalHours;
                    }
                    return (GioKetThuc.Value - GioBatDau.Value).TotalHours;
                }
                return null;
            }
        }
        // Danh sách các loại ca để hiển thị trên giao diện
        public IEnumerable<LoaiCaViewModel> LoaiCaList { get; set; } = new List<LoaiCaViewModel>();
    }
}
