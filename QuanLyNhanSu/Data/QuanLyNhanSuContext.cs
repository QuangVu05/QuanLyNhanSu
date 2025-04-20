using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Data
{
    public class QuanLyNhanSuContext : IdentityDbContext<AppUser>
    {
        public QuanLyNhanSuContext (DbContextOptions<QuanLyNhanSuContext> options)
            : base(options)
        {
        }

        public DbSet<QuanLyNhanSu.Models.BoPhan> BoPhan { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.NhanVien> NhanVien { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.ChucVu> ChucVu { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.DanToc> DanToc { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.PhongBan> PhongBan { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.TonGiao> TonGiao { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.TrinhDo> TrinhDo { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.HopDongLaoDong> HopDongLaoDong { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.LoaiCa> LoaiCa { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.LoaiCong> LoaiCong { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.TienLuong> TienLuong { get; set; } = default!;
        public DbSet<QuanLyNhanSu.Models.TTLamViec> TTLamViec { get; set; } = default!; 
        public DbSet<QuanLyNhanSu.Models.KhenThuong_KyLuat> KhenThuong_KyLuat { get; set; } = default!;
    }
}
