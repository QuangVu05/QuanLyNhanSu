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

namespace QuanLyNhanSu.Controllers
{
    [Authorize(Roles = "Admin, HR Manager")]
    public class TonGiaosController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public TonGiaosController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        // GET: TonGiaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TonGiao.ToListAsync());
        }

        public IActionResult Create() => PartialView("Create");

        // POST: TonGiaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTG,TenTG")] TonGiao tonGiao)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Create", tonGiao);
            }

            if (await EntityHelper.CheckDuplicate(_context, _context.TonGiao, "TenTG", tonGiao.TenTG, "IdTG", null, "Tên tôn giáo đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenTG", error);
                return PartialView("Create", tonGiao);
            }

            // Thêm bộ phận mới vào cơ sở dữ liệu
            bool createSuccess = await EntityHelper.CreateEntity(_context, _context.TonGiao, tonGiao, "tôn giáo",  TempData);
            if (createSuccess)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

        // GET: TonGiaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tonGiao = await _context.TonGiao.FindAsync(id);
            if (tonGiao == null)
            {
                return NotFound();
            }
            return PartialView("Edit",tonGiao);
        }

        // POST: TonGiaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTG,TenTG")] TonGiao tonGiao)
        {
            if (id != tonGiao.IdTG)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return PartialView("Edit", tonGiao);
            }

            if (await EntityHelper.CheckDuplicate(_context, _context.TonGiao, "TenTG", tonGiao.TenTG, "IdTG", id, "Tên tôn giáo đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenTG", error);
                return PartialView("Edit", tonGiao);
            }
            bool isUpdated = await EntityHelper.EditEntity(_context, _context.TonGiao, tonGiao, "tôn giáo",  TempData);

            if (isUpdated)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

        // GET: TonGiaos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.TonGiao, id, "Tôn giáo", TempData);

                if (!isDeleted)
                {
                    TempData["ErrorMessage"] = "Xóa thất bại. Có thể đối tượng không tồn tại hoặc đang được sử dụng.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa: {ex.Message}");
                TempData["ErrorMessage"] = $"Lỗi khi xóa: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: TonGiaos/Delete/5
       

        private bool TonGiaoExists(int id)
        {
            return _context.TonGiao.Any(e => e.IdTG == id);
        }
    }
}
