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
    public class BoPhansController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public BoPhansController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        // GET: BoPhans
        public async Task<IActionResult> Index()
        {
            // Trả về danh sách các bộ phận từ database và một đối tượng BoPhan mới
            var boPhans = await _context.BoPhan.ToListAsync();

            return View(boPhans); // Trả về danh sách bộ phận

        }

        public IActionResult Create() => PartialView("Create");


        // POST: BoPhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBP,TenBP")] BoPhan boPhan)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Create", boPhan);
            }


            if (await EntityHelper.CheckDuplicate(_context, _context.BoPhan, "TenBP", boPhan.TenBP, "IdBP", null, "Tên bộ phận đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenBP", error);
                return PartialView("Create", boPhan);

            }

            // Thêm bộ phận mới vào cơ sở dữ liệu
            bool createSuccess = await EntityHelper.CreateEntity(_context, _context.BoPhan, boPhan, "Bộ phận", TempData);
            if (createSuccess)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }



        // GET: BoPhans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boPhan = await _context.BoPhan.FindAsync(id);
            if (boPhan == null)
            {
                return NotFound();
            }
            return PartialView("Edit", boPhan);
        }

        // POST: BoPhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBP,TenBP")] BoPhan boPhan)
        {
            if (id != boPhan.IdBP)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return PartialView("Edit", boPhan);
            }


            if (await EntityHelper.CheckDuplicate(_context, _context.BoPhan, "TenBP", boPhan.TenBP, "IdBP", id, "Tên bộ phận đã tồn tại trong hệ thống.", TempData))
            {
                string error = TempData["ErrorMessage"] as string ?? "";
                ModelState.AddModelError("TenBP", error);
                return PartialView("Edit", boPhan);

            }

            bool isUpdated = await EntityHelper.EditEntity(_context, _context.BoPhan, boPhan, "Bộ phận", TempData);

            if (isUpdated)
            {
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            else
            {
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }


        }
       

        // GET: BoPhans/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            
          
                bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.BoPhan, id, "Bộ phận", TempData);
                if (!isDeleted)
                {
                    TempData["ErrorMessage"] = "Xóa thất bại. Có thể đối tượng không tồn tại hoặc đang được sử dụng.";
                }
            
            
            return RedirectToAction(nameof(Index));

        }

        // POST: BoPhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boPhan = await _context.BoPhan.FindAsync(id);
            if (boPhan != null)
            {
                _context.BoPhan.Remove(boPhan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoPhanExists(int id)
        {
            return _context.BoPhan.Any(e => e.IdBP == id);
        }
    }
}
