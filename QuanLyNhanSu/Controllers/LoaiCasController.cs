using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Helpers;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.ViewModels;

namespace QuanLyNhanSu.Controllers
{
    public class LoaiCasController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public LoaiCasController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        // GET: LoaiCas
        public async Task<IActionResult> Index()
        {
            
            var viewModel = await GetListLoaiCaViewModelAsync();
            

            return View(viewModel);
            
        }
        public  IActionResult Create() =>  PartialView("_Create");


        // POST: LoaiCas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCa,TenCa,GioBatDau,GioKetThuc,HeSoLuong")] LoaiCaViewModel loaiCaVm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", loaiCaVm);
            }

            
            var isSoHDDuplicate = await EntityHelper.CheckDuplicate(_context, _context.LoaiCa,
                "TenCa", loaiCaVm.TenCa ?? "",
                "IdCa", null,
                "Tên ca đã tồn tại trong hệ thống.", TempData);
            if (isSoHDDuplicate)
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenCa", error);
                return PartialView("_Create", loaiCaVm);
            }
           
            try
            {
                var loaiCa = new LoaiCa
                {
                    TenCa = loaiCaVm.TenCa ?? "",
                    GioBatDau = loaiCaVm.GioBatDau,
                    GioKetThuc = loaiCaVm.GioKetThuc,
                    HeSoLuong = loaiCaVm.HeSoLuong
                };

              
                bool createSuccess = await EntityHelper.CreateEntity(_context, _context.LoaiCa, loaiCa, "loại ca", TempData);
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

        // GET: LoaiCas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiCa = await GetLoaiCaInDbAsync(id.Value);
            if (loaiCa == null)
            {
                return NotFound();
            }
            var viewModel = new LoaiCaViewModel
            {
                IdCa = loaiCa.IdCa,
                TenCa = loaiCa.TenCa,
                GioBatDau = loaiCa.GioBatDau,
                GioKetThuc = loaiCa.GioKetThuc,
                HeSoLuong = loaiCa.HeSoLuong
            };

            return PartialView("_Edit",viewModel);
        }

        // POST: LoaiCas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCa,TenCa,GioBatDau,GioKetThuc,HeSoLuong")] LoaiCaViewModel loaiCaVm)
        {
            
            if (id != loaiCaVm.IdCa) { return NotFound(); }
           
            if (!ModelState.IsValid)
            {
                return PartialView("_Edit", loaiCaVm);
            }

            var isSoHDDuplicate = await EntityHelper.CheckDuplicate(_context, _context.LoaiCa,
                "TenCa", loaiCaVm.TenCa ?? "",
                "IdCa", id,
                "Tên ca đã tồn tại trong hệ thống.", TempData);
            if (isSoHDDuplicate)
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenCa", error);
                return PartialView("_Edit", loaiCaVm);
            }
  
                try
                {
                    
                var loaiCaEdit = await GetLoaiCaInDbAsync(id);
                if (loaiCaEdit == null) { return NotFound(); }
                // Cập nhật dữ liệu

                loaiCaEdit.TenCa = loaiCaVm.TenCa ?? "";
                loaiCaEdit.GioBatDau = loaiCaVm.GioBatDau;
                loaiCaEdit.GioKetThuc = loaiCaVm.GioKetThuc;
                loaiCaEdit.HeSoLuong = loaiCaVm.HeSoLuong;

                    bool createSuccess = await EntityHelper.EditEntity(_context, _context.LoaiCa, loaiCaEdit, "loại ca", TempData);
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

                    Console.WriteLine($"Lỗi : {ex.Message}");
                    Console.WriteLine(ex.StackTrace); // Xem chi tiết lỗi
                    string errorMessage = $"Lỗi xảy ra: {ex.Message}";
                   
                    return Json(new { success = false, errorMessage });
                }

 
        }

        // GET: LoaiCas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.LoaiCa, id, "Loại ca", TempData);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Xóa thất bại. Có thể đối tượng không tồn tại hoặc đang được sử dụng.";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: LoaiCas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiCa = await _context.LoaiCa.FindAsync(id);
            if (loaiCa != null)
            {
                _context.LoaiCa.Remove(loaiCa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiCaExists(int id)
        {
            return _context.LoaiCa.Any(e => e.IdCa == id);
        }
            private async Task<LoaiCaViewModel> GetListLoaiCaViewModelAsync()
            {
             var loaiCa = await _context.LoaiCa.ToListAsync();
            var viewModel = new LoaiCaViewModel
                {
                LoaiCaList = loaiCa.Select(ca => new LoaiCaViewModel
                {
                    IdCa = ca.IdCa,
                    TenCa = ca.TenCa,
                    GioBatDau = ca.GioBatDau,
                    GioKetThuc = ca.GioKetThuc,
                    HeSoLuong = ca.HeSoLuong
                }).ToList()
            };
        
           
            return viewModel;
            }
        private async Task<LoaiCa?> GetLoaiCaInDbAsync(int id)
        {
            // Tìm hợp đồng theo id trong context
            return await _context.LoaiCa.FindAsync(id);
        }
    }
}
