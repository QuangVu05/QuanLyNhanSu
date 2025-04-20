using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.ViewModels
{
    public class HopDongViewModel
    {
        public HopDongLaoDong HopDongMoi { get; set; } = new HopDongLaoDong();
        public IEnumerable<HopDongLaoDong> DanhSachHopDong { get; set; } = new List<HopDongLaoDong>();
       
    }
}
