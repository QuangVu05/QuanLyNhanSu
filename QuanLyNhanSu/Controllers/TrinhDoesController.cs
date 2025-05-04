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
    public class TrinhDoesController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public TrinhDoesController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        // GET: TrinhDoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrinhDo.ToListAsync());
        }

        public IActionResult Create() => PartialView("Create");

        // POST: TrinhDoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTD,TenTD")] TrinhDo trinhDo)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Create", trinhDo);
            }

            if (await EntityHelper.CheckDuplicate(_context, _context.TrinhDo, "TenTD", trinhDo.TenTD, "IdTD", null, "Tên trình độ đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenTD", error);
                return PartialView("Create", trinhDo);
            }
            // Thêm bộ phận mới vào cơ sở dữ liệu
            bool createSuccess = await EntityHelper.CreateEntity(_context, _context.TrinhDo, trinhDo, "Trình độ", TempData);
            if (createSuccess)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

        // GET: TrinhDoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trinhDo = await _context.TrinhDo.FindAsync(id);
            if (trinhDo == null)
            {
                return NotFound();
            }
            return PartialView("Edit",trinhDo);
        }

        // POST: TrinhDoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTD,TenTD")] TrinhDo trinhDo)
        {
            if (id != trinhDo.IdTD)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return PartialView("Edit", trinhDo);
            }


            if (await EntityHelper.CheckDuplicate(_context, _context.TrinhDo, "TenTD", trinhDo.TenTD, "IdTD", id, "Tên trình độ đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenTD", error);
                return PartialView("Edit", trinhDo);
            }
            bool isUpdated = await EntityHelper.EditEntity(_context, _context.TrinhDo, trinhDo,  "Trình độ",  TempData);

            if (isUpdated)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

        // GET: TrinhDoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.TrinhDo, id, "Trình độ", TempData);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Xóa thất bại. Có thể đối tượng không tồn tại hoặc đang được sử dụng.";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: TrinhDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trinhDo = await _context.TrinhDo.FindAsync(id);
            if (trinhDo != null)
            {
                _context.TrinhDo.Remove(trinhDo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrinhDoExists(int id)
        {
            return _context.TrinhDo.Any(e => e.IdTD == id);
        }
    }
}
