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
    public class KhenThuong_KyLuatController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public KhenThuong_KyLuatController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        // GET: KhenThuong_KyLuat
        public IActionResult Index(int? Nam, int? Thang, string LoaiQuyetDinh)
        {
            // Khởi tạo query ban đầu
            var query = _context.KhenThuong_KyLuat.Include(k => k.NhanVien).AsQueryable();

            // Chỉ lọc dữ liệu khi có tham số
            if (Nam.HasValue || Thang.HasValue || !string.IsNullOrEmpty(LoaiQuyetDinh))
            {
                // Lọc theo năm nếu có giá trị
                if (Nam.HasValue)
                {
                    query = query.Where(k => k.Ngay.HasValue && k.Ngay.Value.Year == Nam.Value);
                }

                // Lọc theo tháng nếu có giá trị
                if (Thang.HasValue)
                {
                    query = query.Where(k => k.Ngay.HasValue && k.Ngay.Value.Month == Thang.Value);
                }

                // Lọc theo loại quyết định nếu có giá trị
                if (!string.IsNullOrEmpty(LoaiQuyetDinh))
                {
                    query = query.Where(k => k.LoaiQuyetDinh == LoaiQuyetDinh);
                }
                ViewBag.Nam = Nam;
                ViewBag.Thang = Thang;
                ViewBag.LoaiQuyetDinh = LoaiQuyetDinh;
                // Trả về danh sách đã lọc
                return View(query.ToList());
            }
           
            // Nếu không có tham số lọc, không hiển thị dữ liệu, chỉ hiển thị form
            return View();
        }





        // GET: KhenThuong_KyLuat/Create
        public async Task< IActionResult> Create()
        {
            ViewBag.NhanViens = await GetNhanViensAsync();
            return View();
        }

        // POST: KhenThuong_KyLuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoKTKL,LoaiQuyetDinh,IdNV,Ngay,LyDo,NoiDung")] KhenThuong_KyLuat khenThuong_KyLuat)
        {
           
            if (!ModelState.IsValid)
            {
                // Kiểm tra nếu ngày có giá trị và là ngày trong tương lai
               
                ViewBag.NhanViens = await GetNhanViensAsync();

                return View("Create", khenThuong_KyLuat);
            }
            if (khenThuong_KyLuat.Ngay.Value > DateTime.Now)
            {
                ModelState.AddModelError("Ngay", "Ngày quyết định không thể trong tương lai.");
                ViewBag.NhanViens = await GetNhanViensAsync();

                return View("Create", khenThuong_KyLuat);
            }
            string kt_kl = null;
            // Chuyển đổi mã loại quyết định thành dạng dễ hiểu hơn
            if (khenThuong_KyLuat.LoaiQuyetDinh == "QDKT")
            {
                kt_kl = "khen thưởng";
            }
            else if (khenThuong_KyLuat.LoaiQuyetDinh == "QDKL")
            {
                kt_kl = "kỷ luật";
            }
            
           
            // Tạo Số KTKL tự động
            khenThuong_KyLuat.SoKTKL = await GenerateSoKTKLAsync(khenThuong_KyLuat.LoaiQuyetDinh);
            bool createSuccess = await EntityHelper.CreateEntity(_context, _context.KhenThuong_KyLuat, khenThuong_KyLuat, kt_kl, TempData);
           if (createSuccess)
            {
                ViewBag.NhanViens = await GetNhanViensAsync();
                return View();
            }
            ViewBag.NhanViens = await GetNhanViensAsync();
            return View(khenThuong_KyLuat);

        }

        // GET: KhenThuong_KyLuat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Khen thưởng, kỷ luật không tồn tại.";
            }

            var khenThuong_KyLuat = await _context.KhenThuong_KyLuat
     .Include(k => k.NhanVien) // Load thông tin nhân viên
     .FirstOrDefaultAsync(k => k.Id == id);
            if (khenThuong_KyLuat == null)
            {
                return NotFound();
            }
           return View("Edit", khenThuong_KyLuat);
        }

        // POST: KhenThuong_KyLuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SoKTKL,IdNV,LoaiQuyetDinh,Ngay,LyDo,NoiDung")] KhenThuong_KyLuat khenThuong_KyLuat)
        {
            
            if (id != khenThuong_KyLuat.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
               
                khenThuong_KyLuat.NhanVien = await _context.NhanVien
             .FirstOrDefaultAsync(nv => nv.IdNV == khenThuong_KyLuat.IdNV);
                return View("Edit", khenThuong_KyLuat);
            }
            if (khenThuong_KyLuat.Ngay.Value > DateTime.Now)
            {
                ModelState.AddModelError("Ngay", "Ngày quyết định không thể trong tương lai.");
                khenThuong_KyLuat.NhanVien = await _context.NhanVien
             .FirstOrDefaultAsync(nv => nv.IdNV == khenThuong_KyLuat.IdNV);

                return View("Edit", khenThuong_KyLuat);
            }

            string kt_kl = null;
            if (khenThuong_KyLuat.LoaiQuyetDinh == "QDKT")
            {
                kt_kl = "khen thưởng";
            }
            else if (khenThuong_KyLuat.LoaiQuyetDinh == "QDKL")
            {
                kt_kl = "kỷ luật";
            }
            
            bool editSuccess = await EntityHelper.EditEntity(_context, _context.KhenThuong_KyLuat, khenThuong_KyLuat, kt_kl, TempData);
            if (editSuccess)
            {
              
                return RedirectToAction("Index");
            }
            khenThuong_KyLuat.NhanVien = await _context.NhanVien
            .FirstOrDefaultAsync(nv => nv.IdNV == khenThuong_KyLuat.IdNV);
            return View(khenThuong_KyLuat);
            
        }

        // GET: KhenThuong_KyLuat/Delete/5
       

        // POST: KhenThuong_KyLuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
           
            Console.WriteLine($"URL ID: {id}" );
           
            bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.KhenThuong_KyLuat, id, "thông tin", TempData);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Xóa thất bại. Có thể đối tượng không tồn tại. ";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool KhenThuong_KyLuatExists(string id)
        {
            return _context.KhenThuong_KyLuat.Any(e => e.SoKTKL == id);
        }
        private async Task<SelectList> GetNhanViensAsync()
        {
            return new SelectList(_context.NhanVien.ToList(), "IdNV", "MaVaTen");

        }
        public async Task<string> GenerateSoKTKLAsync(string loaiQuyetDinh)
        {
            if (string.IsNullOrEmpty(loaiQuyetDinh))
            {
                throw new ArgumentException("Loại quyết định không được để trống.");
            }

            string year = DateTime.Now.Year.ToString();

            // Lấy bản ghi mới nhất theo loại quyết định & năm
            var lastRecord = await _context.KhenThuong_KyLuat
                .Where(x => x.LoaiQuyetDinh == loaiQuyetDinh && x.SoKTKL.EndsWith($"/{year}/{loaiQuyetDinh}"))
                .OrderByDescending(x => x.Id) // Sắp xếp theo Id giảm dần
                .FirstOrDefaultAsync();

            int nextNumber = 1; // Mặc định 0001 nếu chưa có bản ghi nào

            if (lastRecord != null)
            {
                string lastNumberPart = lastRecord.SoKTKL.Split('/')[0]; // "0005/2025/QDKT" -> "0005"

                if (int.TryParse(lastNumberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{nextNumber:D4}/{year}/{loaiQuyetDinh}";
        }


    }
}
