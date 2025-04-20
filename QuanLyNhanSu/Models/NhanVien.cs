using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyNhanSu.Models
{
    public class NhanVien
    {
       
        [Key]
        public int IdNV { get; set; }
        [StringLength(10, MinimumLength = 8, ErrorMessage = "Mã nhân viên phải có từ 8 đến 10 ký tự.")]
        public string? MaNV { get; set; }
        [StringLength(255)]
        public  string? Anh { get; set; }
        [Required(ErrorMessage = "Tên nhân viên là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên nhân viên không được vượt quá 100 ký tự.")]
        public string? TenNV { get; set; }

        [Required(ErrorMessage = "Giới tính là bắt buộc.")]
        [ForeignKey("GioiTinh")]
        public int? IdGioiTinh { get; set; }
        public virtual GioiTinh? GioiTinh { get; set; }

        [Required(ErrorMessage = "Chứng minh nhân dân hoặc căn cước công dân là bắt buộc.")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "CCCD phải có từ 9 đến 12 ký tự.")]
        public string? CCCD { get; set; }
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có từ 10 đến 15 chữ số.")]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        public string? SDT { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        public DateTime? NgaySinh { get; set; }

        [Required(ErrorMessage = "Quốc tịch là bắt buộc.")]
        
        [StringLength(50, ErrorMessage = "Quốc tịch không được vượt quá 50 ký tự.")]
        public string? QuocTich { get; set; }

        [StringLength(100, ErrorMessage = "Nơi sinh không được vượt quá 100 ký tự.")]
        public string? NoiSinh { get; set; }

        [StringLength(150, ErrorMessage = "Hộ khẩu không được vượt quá 150 ký tự.")]
        public string? HoKhau { get; set; }

        [StringLength(100, ErrorMessage = "Quê quán không được vượt quá 100 ký tự.")]
        public string? QueQuan { get; set; }

        [StringLength(150, ErrorMessage = "Tạm trú không được vượt quá 150 ký tự.")]
        public string? TamChu { get; set; }
        [Required(ErrorMessage = "Loại nhân viên là bắt buộc.")]
        // Sử dụng khóa ngoại với bảng tham chiếu
        [ForeignKey("LoaiNhanVien")]
        public int IdLoaiNV { get; set; }
        public virtual LoaiNhanVien? LoaiNhanVien { get; set; }
       
        [ForeignKey("TTHonNhan")]
        public int? IdTTHonNhan{ get; set; }
        public virtual TTHonNhan? TTHonNhan { get; set; }
        
        [ForeignKey("TTLamViec")]
        public int? IdTTLamViec { get; set; }
        public virtual TTLamViec? TTLamViec { get; set; }

        [Required(ErrorMessage = "Bộ phận là bắt buộc.")]
        [ForeignKey("BoPhan")]
        public int IdBP { get; set; }
        public virtual BoPhan? BoPhan { get; set; }

        [Required(ErrorMessage = "Dân tộc là bắt buộc.")]
        [ForeignKey("DanToc")]
        public int IdDT { get; set; }
        public virtual DanToc? DanToc { get; set; }

        // Khóa ngoại đến Phòng Ban
        [Required(ErrorMessage = "Phòng ban là bắt buộc.")]
        [ForeignKey("PhongBan")]
        public int IdPB { get; set; }
        public virtual PhongBan? PhongBan { get; set; }

        // Khóa ngoại đến Chức Vụ
        [ForeignKey("ChucVu")]
        [Required(ErrorMessage = "Chức vụ là bắt buộc.")]
        public int IdCV { get; set; }
        public virtual ChucVu? ChucVu { get; set; }
        
        [ForeignKey("TonGiao")]
        public int? IdTG { get; set; }
        public virtual TonGiao? TonGiao { get; set; }

        [ForeignKey("TrinhDo")]
        public int? IdTD { get; set; }
        public virtual TrinhDo? TrinhDo { get; set; }
        public virtual ICollection<HopDongLaoDong>? HopDongLaoDongs { get; set; } // Quan hệ 1-N
        public virtual ICollection<TienLuong>? TienLuongs { get; set; } // Quan hệ 1-N
        public virtual ICollection<KhenThuong_KyLuat>? KhenThuongKyLuats { get; set; }
        public string MaVaTen => $"{MaNV} - {TenNV}";
        // 🔹 Thêm khóa ngoại liên kết với AspNetUsers
        [ForeignKey("User")]
        public string? IdUser { get; set; }
        public virtual AppUser? User { get; set; } // Liên kết với bảng AspNetUsers

    }
}
