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
    public class DanTocsController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public DanTocsController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        // GET: DanTocs
        public async Task<IActionResult> Index()
        {
            return View(await _context.DanToc.ToListAsync());
        }

        public IActionResult Create() => PartialView("Create");


        // POST: DanTocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDT,TenDT")] DanToc danToc)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Create", danToc);
            }

            if (await EntityHelper.CheckDuplicate(_context, _context.DanToc, "TenDT", danToc.TenDT, "IdDT", null, "Tên dân tộc đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenDT", error);
                return PartialView("Create", danToc);
            }
            // Thêm bộ phận mới vào cơ sở dữ liệu
            bool createSuccess = await EntityHelper.CreateEntity(_context, _context.DanToc, danToc, "dân tộc",  TempData);
            if (createSuccess)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

        // GET: DanTocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danToc = await _context.DanToc.FindAsync(id);
            if (danToc == null)
            {
                return NotFound();
            }
            return PartialView("Edit",danToc);
        }

        // POST: DanTocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDT,TenDT")] DanToc danToc)
        {
            if (id != danToc.IdDT)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return PartialView("Edit", danToc);
            }

            if (await EntityHelper.CheckDuplicate(_context, _context.DanToc, "TenDT", danToc.TenDT, "IdDT", id, "Tên dân tộc đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenDT", error);
                return PartialView("Edit", danToc);
            }
            bool isUpdated = await EntityHelper.EditEntity(_context, _context.DanToc, danToc,  "dân tộc",  TempData);

            if (isUpdated)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

        // GET: DanTocs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.DanToc, id, "Dân tộc", TempData);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Xóa thất bại. Có thể đối tượng không tồn tại hoặc đang được sử dụng.";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: DanTocs/Delete/5
       

        private bool DanTocExists(int id)
        {
            return _context.DanToc.Any(e => e.IdDT == id);
        }
    }
}
