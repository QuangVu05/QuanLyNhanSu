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
    public class ChucVusController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public ChucVusController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        // GET: ChucVus
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChucVu.ToListAsync());
        }


        public IActionResult Create() => PartialView("Create");

        // POST: ChucVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCV,TenCV")] ChucVu chucVu)
        {

            if (!ModelState.IsValid)
            {
                return PartialView("Create", chucVu);
            }


            if (await EntityHelper.CheckDuplicate(_context, _context.ChucVu, "TenCV", chucVu.TenCV, "IdCV", null, "Tên chức vụ đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenCV", error);
                return PartialView("Create", chucVu);
            }
            // Thêm bộ phận mới vào cơ sở dữ liệu
            bool createSuccess = await EntityHelper.CreateEntity(_context, _context.ChucVu, chucVu, "chức vụ",  TempData);
            if (createSuccess)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

        // GET: ChucVus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVu.FindAsync(id);
            if (chucVu == null)
            {
                return NotFound();
            }
            return PartialView("Edit",chucVu);
        }

        // POST: ChucVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCV,TenCV")] ChucVu chucVu)
        {
            if (id != chucVu.IdCV)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return PartialView("Create", chucVu);
            }


            if (await EntityHelper.CheckDuplicate(_context, _context.ChucVu, "TenCV", chucVu.TenCV, "IdCV", id, "Tên chức vụ đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenCV", error);
                return PartialView("Create", chucVu);
            }
            bool isUpdated = await EntityHelper.EditEntity(_context, _context.ChucVu, chucVu,  "chức vụ",  TempData);

            if (isUpdated)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

        // GET: ChucVus/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.ChucVu, id, "Chức vụ", TempData);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Xóa thất bại. Có thể đối tượng không tồn tại hoặc đang được sử dụng.";
            }

            return RedirectToAction(nameof(Index));
        }

       

        private bool ChucVuExists(int id)
        {
            return _context.ChucVu.Any(e => e.IdCV == id);
        }
    }
}
