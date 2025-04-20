using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Helpers;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.ViewModels;

namespace QuanLyNhanSu.Controllers
{
    [Authorize(Roles = "Admin, HR Manager")]
    public class HopDongLaoDongsController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public HopDongLaoDongsController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        private void SetDropdownLists()
        {
            ViewBag.IdNV = new SelectList(_context.NhanVien, "IdNV", "MaVaTen");

        }
        // GET: HopDongLaoDongs
        public async Task<IActionResult> Index()
        {
           
            var viewModel =  await GetListHopDongViewModelAsync();
            SetDropdownLists();
            return View(viewModel);
        }

        // GET: HopDongLaoDongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDongLaoDong = await _context.HopDongLaoDong
                .Include(h => h.NhanVien)
                .FirstOrDefaultAsync(m => m.IdHD == id);
            if (hopDongLaoDong == null)
            {
                TempData["ErrorMessage"] = "Hợp đồng không tồn tại.";

                return RedirectToAction(nameof(Index));
            }
            
           

            return View(hopDongLaoDong);
        }

        public IActionResult Create()
        {    
            SetDropdownLists();
            return View();
        }
        // POST: HopDongLaoDongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( HopDongViewModel model)
        {
            var hopDongLaoDong = model.HopDongMoi;
            // Kiểm tra điều kiện ngày ký và ngày bắt đầu
            
            ValidateHopDongDates(hopDongLaoDong);
            if (ModelState.IsValid)
            {
                try
                {
                    var isSoHDDuplicate = await EntityHelper.CheckDuplicate(_context, _context.HopDongLaoDong,
                "SoHD", hopDongLaoDong.SoHD,
                "IdHD", null,
                "Số hợp đồng đã tồn tại trong hệ thống.", TempData);
                    if (isSoHDDuplicate)
                    {
                        var dsHopDong = await GetListHopDongViewModelAsync();
                        model.DanhSachHopDong =dsHopDong.DanhSachHopDong;
                            
                        SetDropdownLists();
                        return View( model);
                    }
                    bool createSuccess = await EntityHelper.CreateEntity(_context, _context.HopDongLaoDong, hopDongLaoDong, "hợp đồng", TempData);
                    if (createSuccess)
                    {

                     
                        return RedirectToAction("Index"); // Hoặc trang danh sách nhân viên
                    }
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine($"Lỗi : {ex.Message}");
                    Console.WriteLine(ex.StackTrace); // Xem chi tiết lỗi
                    TempData["ErrorMessage"] = $"Lỗi xảy ra: {ex.Message}";
                }

            }
            
            
            // Hiển thị lại form nếu có lỗi
            TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm hợp đồng. Vui lòng thử lại.";
            var dsHopDongReload = await GetListHopDongViewModelAsync();
            model.DanhSachHopDong = dsHopDongReload.DanhSachHopDong;
            SetDropdownLists();
            return View( model);
        }

        // GET: HopDongLaoDongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDongLaoDong = await GetHopDongInDbAsync(id.Value);
            if (hopDongLaoDong == null)
            {
                TempData["ErrorMessage"] = "Hợp đồng không tồn tại.";

                return RedirectToAction(nameof(Index));
            }
            var viewModel = new HopDongViewModel
            {
                HopDongMoi = hopDongLaoDong
            };
            SetDropdownLists();
            return View(viewModel);
        }

        // POST: HopDongLaoDongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HopDongViewModel model)
        {
            var hopDongLaoDong = model.HopDongMoi;
            if (id != hopDongLaoDong.IdHD)
            {
                return NotFound();
            }
            ValidateHopDongDates(hopDongLaoDong);
         
            if (ModelState.IsValid)
            {
                try
                {
                    var isSoHDDuplicate = await EntityHelper.CheckDuplicate(_context, _context.HopDongLaoDong,
                     "SoHD", hopDongLaoDong.SoHD,
                     "IdHD", id,
                     "Số hợp đồng đã tồn tại trong hệ thống.", TempData);
                    if (isSoHDDuplicate)
                    {
                        SetDropdownLists();
                        return View(hopDongLaoDong);
                    }

                    var hopDongInDb = await GetHopDongInDbAsync(id);
                    if (hopDongInDb == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật dữ liệu từ model.HopDongMoi sang bản ghi trong DB
                    hopDongInDb.SoHD = model.HopDongMoi.SoHD;
                    hopDongInDb.IdNV = model.HopDongMoi.IdNV;
                    hopDongInDb.NgayKy = model.HopDongMoi.NgayKy;
                    hopDongInDb.NgayBatDau = model.HopDongMoi.NgayBatDau;
                    hopDongInDb.NgayKetThuc = model.HopDongMoi.NgayKetThuc;
                    hopDongInDb.LoaiHopDong = model.HopDongMoi.LoaiHopDong;
                    hopDongInDb.LuongCoBan = model.HopDongMoi.LuongCoBan;
                    hopDongInDb.HeSoLuong = model.HopDongMoi.HeSoLuong;
                    hopDongInDb.NoiDung = model.HopDongMoi.NoiDung;
                   

                    bool createSuccess = await EntityHelper.EditEntity(_context, _context.HopDongLaoDong, hopDongInDb, "hợp đồng", TempData);
                    if (createSuccess)
                    {

                        SetDropdownLists();
                        return RedirectToAction("Index"); // Hoặc trang danh sách nhân viên
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Lỗi : {ex.Message}");
                    Console.WriteLine(ex.StackTrace); // Xem chi tiết lỗi
                    TempData["ErrorMessage"] = $"Lỗi xảy ra: {ex.Message}";
                }
            }
            TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật hợp đồng. Vui lòng thử lại.";
            SetDropdownLists();
            return View(model);
        }

        // GET: HopDongLaoDongs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
           
            bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.HopDongLaoDong, id, "Hợp đồng", TempData);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Xóa thất bại. Có thể đối tượng không tồn tại hoặc đang được sử dụng.";
            }

            return RedirectToAction(nameof(Index));
        }

      

        private bool HopDongLaoDongExists(int id)
        {
            return _context.HopDongLaoDong.Any(e => e.IdHD == id);
        }
        private async Task<HopDongLaoDong?> GetHopDongInDbAsync(int id)
        {
            // Tìm hợp đồng theo id trong context
            return await _context.HopDongLaoDong.FindAsync(id);
        }
        private async Task<HopDongViewModel> GetListHopDongViewModelAsync()
        {
            var danhSachHopDong = await _context.HopDongLaoDong
                                                 .Include(h => h.NhanVien)
                                                 .ToListAsync();
            var viewModel = new HopDongViewModel
            {
                DanhSachHopDong = danhSachHopDong
            };
            return viewModel;
        }
        private void ValidateHopDongDates(HopDongLaoDong hopDong)
        {
            // Kiểm tra ngày ký không được là ngày trong tương lai.
            if (hopDong.NgayKy > DateTime.Now)
            {
                ModelState.AddModelError("HopDongMoi.NgayKy", "Ngày ký không được là ngày trong tương lai.");
            }

            // Kiểm tra ngày bắt đầu phải sau hoặc bằng ngày ký.
            if (hopDong.NgayBatDau < hopDong.NgayKy)
            {
                ModelState.AddModelError("HopDongMoi.NgayBatDau", "Ngày bắt đầu phải sau hoặc bằng ngày ký.");
            }

            // Kiểm tra ngày kết thúc của hợp đồng lao động phải lớn hơn ngày bắt đầu.
            if (hopDong.NgayBatDau >= hopDong.NgayKetThuc)
            {
                ModelState.AddModelError("HopDongMoi.NgayKetThuc", "Ngày kết thúc phải lớn hơn ngày bắt đầu.");
            }
        }

    }

}
