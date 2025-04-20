using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Helpers;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.ViewModels;

namespace QuanLyNhanSu.Controllers
{
    public class LoaiCongsController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public LoaiCongsController(QuanLyNhanSuContext context)
        {
            _context = context;
        }
        // Hiển thị danh sách
        public async Task<IActionResult> Index()
        {
            var loaiCongs = await _context.LoaiCong.ToListAsync();
            return View(loaiCongs);
        }

        // Hiển thị form thêm
        public IActionResult Create()
        {
            return PartialView("_CreateLoaiCong");
        }

        // Xử lý thêm mới
        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdCong,TenCong,HeSoLuong")] LoaiCong model)
        {
            // Trả về partial view để modal hiển thị lỗi
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateLoaiCong", model);
            }

            var isDuplicate = await EntityHelper.CheckDuplicate(_context, _context.LoaiCong,
                         "TenCong", model.TenCong ?? "",
                         "IdCong", null,
                         "Tên công đã tồn tại trong hệ thống.", TempData);
            if (isDuplicate)
            {
                string error = TempData["ErrorMessage"] as string ?? "";

                ModelState.AddModelError("TenCong", error);
                return PartialView("_CreateLoaiCong", model);


            }
            try
            {

                var loaiCong = new LoaiCong
                {
                    TenCong = model.TenCong,
                    HeSoLuong = model.HeSoLuong
                };

                bool createSuccess = await EntityHelper.CreateEntity(_context, _context.LoaiCong, loaiCong, "loại công", TempData);

            // Lấy thông báo từ TempData
            if (createSuccess)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                string errorMessage = $"Lỗi xảy ra: {ex.Message}";
                return Json(new { success = false, errorMessage });
            }



        }

        // Hiển thị form sửa
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var loaiCong = await GetInDbAsync(id.Value);
            if (loaiCong == null) return NotFound();

           
            return PartialView("_EditLoaiCong", loaiCong);
        }

        // Xử lý cập nhật
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCong,TenCong,HeSoLuong")] LoaiCong model)
        {
            if (id != model.IdCong) { return NotFound(); }
            var isDuplicate = await EntityHelper.CheckDuplicate(_context, _context.LoaiCong,
                         "TenCong", model.TenCong ?? "",
                         "IdCong", id,
                         "Tên công đã tồn tại trong hệ thống.", TempData);
            if (isDuplicate)
            {
                string error = TempData["ErrorMessage"] as string ?? "";

                ModelState.AddModelError("TenCong", error);


            }
            // Trả về partial view để modal hiển thị lỗi
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateLoaiCong", model);
            }
            try
            {
                var loaiCong= await GetInDbAsync(id);
            if (loaiCong == null) { return NotFound(); }

            loaiCong.TenCong = model.TenCong;
            loaiCong.HeSoLuong = model.HeSoLuong;

            bool createSuccess = await EntityHelper.EditEntity(_context, _context.LoaiCong, loaiCong, "loại công", TempData);

            if (createSuccess)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
             }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                string errorMessage = $"Lỗi xảy ra: {ex.Message}";
                return Json(new { success = false, errorMessage});
            }
        }
        private async Task<LoaiCong?> GetInDbAsync(int id)
        {
            // Tìm hợp đồng theo id trong context
            return await _context.LoaiCong.FindAsync(id);
        }
        // GET: LoaiCongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.LoaiCong, id.Value, "Loại công", TempData);

            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Xóa thất bại. Có thể đối tượng không tồn tại hoặc đang được sử dụng.";
            }
            return RedirectToAction(nameof(Index));

            
        }

    }

    
}

