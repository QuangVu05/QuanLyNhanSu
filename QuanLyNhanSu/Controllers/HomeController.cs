using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.ViewModels;
using System.Diagnostics;

namespace QuanLyNhanSu.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanLyNhanSuContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(ILogger<HomeController> logger, QuanLyNhanSuContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
       
        public IActionResult Index()
        {
            var phongBanData = _context.PhongBan
       .Select(pb => new
       {
           TenPhongBan = pb.TenPB,
           DangLamViec = _context.NhanVien.Count(nv => nv.IdPB == pb.IdPB && nv.IdTTLamViec == 1),
           TamNghiViec = _context.NhanVien.Count(nv => nv.IdPB == pb.IdPB && nv.IdTTLamViec == 2)
       })
       .ToList();
            var model = new TongQuanViewModel
            {
                TongSoNhanVien = _context.NhanVien.Count(),
                TongSoPhongBan = _context.PhongBan.Count(),
                TongSoHopDong = _context.HopDongLaoDong.Count(),
                TongSoTaiKhoan = _context.Users.Count(),
                NhanVienDaNghiViec = _context.NhanVien.Count(nv => nv.IdTTLamViec == 3),
                NhanVienTamNghi = _context.NhanVien.Count(nv => nv.IdTTLamViec == 2),
                NhanVienDangLamViec = _context.NhanVien.Count(nv => nv.IdTTLamViec == 1),
                DanhSachPhongBan = phongBanData.Select(pb => pb.TenPhongBan).ToList(),
                NhanVienDangLamTheoPhongBan = phongBanData.Select(pb => pb.DangLamViec).ToList(),
                NhanVienTamNghiTheoPhongBan = phongBanData.Select(pb => pb.TamNghiViec).ToList()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
