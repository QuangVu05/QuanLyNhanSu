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
    public class PhongBansController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public PhongBansController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        // GET: PhongBans
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhongBan.ToListAsync());
        }

        public IActionResult Create() => PartialView("Create");

        // POST: PhongBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPB,TenPB")] PhongBan phongBan)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Create", phongBan);
            }


            if (await EntityHelper.CheckDuplicate(_context, _context.PhongBan, "TenPB", phongBan.TenPB, "IdPB", null, "Tên phòng ban đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenPB", error);
                return PartialView("Create", phongBan);
            }
            // Thêm bộ phận mới vào cơ sở dữ liệu
            bool createSuccess = await EntityHelper.CreateEntity(_context, _context.PhongBan, phongBan, "Phòng ban", TempData);
            if (createSuccess)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

        // GET: PhongBans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongBan = await _context.PhongBan.FindAsync(id);
            if (phongBan == null)
            {
                return NotFound();
            }
            return PartialView("Edit", phongBan);
        }

        // POST: PhongBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPB,TenPB")] PhongBan phongBan)
        {
            if (id != phongBan.IdPB)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return PartialView("Edit", phongBan);
            }
            if (await EntityHelper.CheckDuplicate(_context, _context.PhongBan, "TenPB", phongBan.TenPB, "IdPB", id, "Tên phòng ban đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenPB", error);
                return PartialView("Edit", phongBan);
            }
            bool isUpdated = await EntityHelper.EditEntity(_context, _context.PhongBan, phongBan, "Phòng ban", TempData);

            if (isUpdated)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

        // GET: PhongBans/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.PhongBan, id, "Phòng ban", TempData);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Xóa thất bại. Có thể đối tượng không tồn tại hoặc đang được sử dụng.";
            }

            return RedirectToAction(nameof(Index));
        }

       

        private bool PhongBanExists(int id)
        {
            return _context.PhongBan.Any(e => e.IdPB == id);
        }
    }
}
