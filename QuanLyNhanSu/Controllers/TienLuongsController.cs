using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Helpers;
using QuanLyNhanSu.Models;
using X.PagedList.Extensions;

namespace QuanLyNhanSu.Controllers
{
    [Authorize(Roles = "Admin, Accountant")]
    public class TienLuongsController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public TienLuongsController(QuanLyNhanSuContext context)
        {
            _context = context;
        }
        private void SetDropdownLists()
        {
            ViewBag.IdNV = new SelectList(_context.NhanVien, "IdNV", "MaVaTen");


        }
        // GET: TienLuongs
        public async Task<IActionResult> Index(int? Nam, int? Thang)
        {
            // Nếu không có bộ lọc, không cần query database
            if (!Nam.HasValue && !Thang.HasValue)
            {

                return View(new List<TienLuong>());
            }
            var danhSachLuong = _context.TienLuong
         .Include(t => t.NhanVien)
         .Include(t => t.HopDongLaoDong)
         .AsQueryable();


            if (Nam.HasValue)
            {
                danhSachLuong = danhSachLuong.Where(t => t.Nam == Nam.Value);
            }

            if (Thang.HasValue)
            {
                danhSachLuong = danhSachLuong.Where(t => t.Thang == Thang.Value);
            }

            ViewBag.Nam = Nam;
            ViewBag.Thang = Thang;

            return View(await danhSachLuong.ToListAsync());
        }


        // GET: TienLuongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tienLuong = await GetTienLuongByIdAsync(id.Value);
            if (tienLuong == null)
            {
                return NotFound();
            }
            return View(tienLuong);
        }

        // GET: TienLuongs/Create

        public IActionResult Create()
        {
            SetDropdownLists();
            return View();
        }

        // POST: TienLuongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTL,IdNV,Thang,Nam,SoNgayCong,LuongTangCa,LuongDaUng")] TienLuong tienLuong)
        {

            DateTime now = DateTime.Now;



            var hopDongMoiNhat = await LayHopDongMoiNhat(tienLuong.IdNV);
            if (hopDongMoiNhat == null)
            {
                ModelState.AddModelError("IdNV", "Nhân viên này chưa có hợp đồng lao động!");
            }
            else
            {
                
                if (hopDongMoiNhat.NgayKetThuc.HasValue && hopDongMoiNhat.NgayKetThuc.Value < now)
                {
                    ModelState.AddModelError("IdNV", "Hợp đồng lao động của nhân viên này đã hết hạn!");
                }
            }

            bool isDuplicate = await _context.TienLuong
                .AnyAsync(tl => tl.IdNV == tienLuong.IdNV && tl.Thang == tienLuong.Thang && tl.Nam == tienLuong.Nam);

            if (isDuplicate)
            {
                TempData["ErrorMessage"] = $"Tiền lương cho nhân viên trong {tienLuong.Thang}-{tienLuong.Nam} đã tính.";
                SetDropdownLists();
                return View(tienLuong);
            }
            // Kiểm tra nếu tháng/năm lớn hơn thời gian thực
            if (tienLuong.Nam > now.Year || (tienLuong.Nam == now.Year && tienLuong.Thang > now.Month))
            {
                TempData["ErrorMessage"] = ("Tháng và năm không được lớn hơn thời gian hiện tại.");

                SetDropdownLists();
                return View(tienLuong);
            }

            // Nếu có lỗi, quay lại form nhập dữ liệu
            if (!ModelState.IsValid || isDuplicate)
            {
                SetDropdownLists();
                return View(tienLuong);
            }

            // Tính toán tổng lương
            tienLuong.TongLuong = TinhTongLuong(hopDongMoiNhat, tienLuong);
            if (hopDongMoiNhat?.IdHD != null)
                tienLuong.IdHD = hopDongMoiNhat.IdHD;

            bool createSuccess = await EntityHelper.CreateEntity(_context, _context.TienLuong, tienLuong, "tiền lương", TempData);
            if (createSuccess)
            {
                SetDropdownLists();
                return View(tienLuong); // Điều hướng về danh sách lương sau khi tạo thành công
            }

            // Nếu tạo thất bại, quay lại form nhập dữ liệu
            SetDropdownLists();
            return View(tienLuong);
        }

        // GET: TienLuongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tienLuong = await _context.TienLuong
                .Include(tl => tl.NhanVien)
                .Include(tl => tl.HopDongLaoDong)
                .FirstOrDefaultAsync(tl => tl.IdTL == id);
            if (tienLuong.HopDongLaoDong == null)
            {
                Console.WriteLine("⚠ Hợp đồng lao động không tồn tại!");
            }
            if (tienLuong == null)
            {
                return NotFound();
            }
            return View(tienLuong);
        }

        // POST: TienLuongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTL,IdNV,IdHD,Thang,Nam,SoNgayCong,LuongTangCa,LuongDaUng")] TienLuong tienLuong)
        {
            if (id != tienLuong.IdTL)
            {
                return NotFound();
            }
            var now = DateTime.Now;

            var tl = await _context.TienLuong.FindAsync(id);
            if (tl == null)
            {
                return NotFound();
            }

            var hopDongMoiNhat = await LayHopDongMoiNhat(tienLuong.IdNV);
            if (hopDongMoiNhat == null)
            {
                ModelState.AddModelError("IdNV", "Nhân viên này chưa có hợp đồng lao động!");
            }
            
            else
            {

                if (hopDongMoiNhat.NgayKetThuc.HasValue && hopDongMoiNhat.NgayKetThuc.Value < now)
                {
                    ModelState.AddModelError("IdNV", "Hợp đồng lao động của nhân viên này đã hết hạn!");
                }
            }

            bool isDuplicate = await _context.TienLuong
                .AnyAsync(tl => tl.IdNV == tienLuong.IdNV && tl.Thang == tienLuong.Thang && tl.Nam == tienLuong.Nam && tl.IdTL != id);

            if (isDuplicate)
            {
                TempData["ErrorMessage"] = $"Tiền lương cho nhân viên trong {tienLuong.Thang}-{tienLuong.Nam} đã tồn tại.";
                await LoadRelatedData(tienLuong);
                return View(tienLuong);
            }

            // Kiểm tra nếu tháng/năm lớn hơn thời gian thực
            if (tienLuong.Nam > now.Year || (tienLuong.Nam == now.Year && tienLuong.Thang > now.Month))
            {
                TempData["ErrorMessage"] = "Tháng và năm không được lớn hơn thời gian hiện tại.";
                await LoadRelatedData(tienLuong);
                return View(tienLuong);
            }

            // Nếu có lỗi, quay lại form nhập dữ liệu
            if (!ModelState.IsValid)
            {
                await LoadRelatedData(tienLuong);
                return View(tienLuong);
            }

            // Cập nhật thông tin từ form vào entity
            tl.Thang = tienLuong.Thang;
            tl.Nam = tienLuong.Nam;
            tl.SoNgayCong = tienLuong.SoNgayCong;
            tl.LuongTangCa = tienLuong.LuongTangCa;
            tl.LuongDaUng = tienLuong.LuongDaUng;


            tl.TongLuong = TinhTongLuong(hopDongMoiNhat, tienLuong);
            if (tl.IdHD != hopDongMoiNhat?.IdHD && hopDongMoiNhat?.IdHD != null)
            {
                tl.IdHD = hopDongMoiNhat.IdHD;
            }

            bool editSuccess = await EntityHelper.EditEntity(_context, _context.TienLuong, tl, "tiền lương", TempData);
            if (editSuccess)
            {
                return RedirectToAction(nameof(Index), new { nam = tienLuong.Nam, thang = tienLuong.Thang });
            }

            // Nếu tạo thất bại, quay lại form nhập dữ liệu
            await LoadRelatedData(tienLuong);
            return View(tienLuong);

        }

        // GET: TienLuongs/Delete/5

        [HttpGet]

        public async Task<IActionResult> Delete(int id, int? thang, int? nam)
        {
            bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.TienLuong, id, "Tiền lương", TempData);


            return RedirectToAction(nameof(Index), new { thang, nam });
        }
        [HttpPost]
        public async Task<IActionResult> ToggleNhanTien(int id, int? thang, int? nam)
        {
            var tienLuong = await _context.TienLuong.FindAsync(id);
            if (tienLuong == null)
            {
                return NotFound();
            }

            if (tienLuong.DaNhanTien)
            {
                tienLuong.DaNhanTien = false;
                tienLuong.NgayNhanTien = null; // Xóa ngày nhận tiền
            }
            else
            {
                tienLuong.DaNhanTien = true;
                tienLuong.NgayNhanTien = DateTime.Today; // Chỉ lấy ngày, không có giờ
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { thang, nam });
        }
        private bool TienLuongExists(int id)
        {
            return _context.TienLuong.Any(e => e.IdTL == id);
        }


        public IActionResult ExportToExcel(int thang, int nam)
        {
            var danhSachTienLuong = _context.TienLuong
                .Where(t => t.Thang == thang && t.Nam == nam)
                .Select(t => new
                {
                    MaNV = t.NhanVien != null ? t.NhanVien.MaNV : "null",
                    TenNV = t.NhanVien != null ? t.NhanVien.TenNV : "null",
                    t.Thang,
                    t.Nam,
                    t.SoNgayCong,
                    HeSoLuong = t.HopDongLaoDong != null ? t.HopDongLaoDong.HeSoLuong : 0,
                    LuongCoBan = t.HopDongLaoDong != null ? t.HopDongLaoDong.LuongCoBan : 0,
                    t.LuongTangCa,
                    t.LuongDaUng,
                    t.TongLuong,
                    DaNhanTien = t.DaNhanTien ? "Đã nhận" : "Chưa nhận",
                    NgayNhanTien = t.NgayNhanTien.HasValue ? t.NgayNhanTien.Value.ToString("dd/MM/yyyy") : "Chưa nhận"
                })
                .ToList();
            if (danhSachTienLuong.Count == 0)
            {
                TempData["ErrorMessage"] = "Không có dữ liệu để xuất.";

                return RedirectToAction(nameof(Index), new { thang, nam});
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add($"TienLuong_{thang}_{nam}");
                var currentRow = 1;

                // Danh sách tiêu đề cột
                string[] headers = { "STT", "Mã NV", "Tên nhân viên", "Tháng", "Năm", "Số ngày công", "Hệ số lương",
                         "Lương cơ bản", "Lương tăng ca", "Lương đã ưng", "Tổng lương",
                         "Nhận Lương", "Ngày nhận lương" };

                // Dùng vòng lặp để tạo tiêu đề cột
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cell(currentRow, i + 1).Value = headers[i];
                }

                // Định dạng tiêu đề
                var headerRow = worksheet.Range($"A{currentRow}:M{currentRow}");
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Ghi dữ liệu
                int stt = 1;
                foreach (var item in danhSachTienLuong)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = stt++;
                    worksheet.Cell(currentRow, 2).Value = item.MaNV;
                    worksheet.Cell(currentRow, 3).Value = item.TenNV;
                    worksheet.Cell(currentRow, 4).Value = item.Thang;
                    worksheet.Cell(currentRow, 5).Value = item.Nam;
                    worksheet.Cell(currentRow, 6).Value = item.SoNgayCong;
                    worksheet.Cell(currentRow, 7).Value = item.HeSoLuong;
                    worksheet.Cell(currentRow, 8).Value = item.LuongCoBan;
                    worksheet.Cell(currentRow, 9).Value = item.LuongTangCa;
                    worksheet.Cell(currentRow, 10).Value = item.LuongDaUng;
                    worksheet.Cell(currentRow, 11).Value = item.TongLuong;
                    worksheet.Cell(currentRow, 12).Value = item.DaNhanTien;
                    worksheet.Cell(currentRow, 13).Value = item.NgayNhanTien;
                }

                // Auto-fit cột để hiển thị đầy đủ nội dung
                worksheet.Columns().AdjustToContents();


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"TienLuong_{thang}_{nam}.xlsx");
                }

            }
        }


        public async Task<IActionResult> GetHopDongMoiNhat(int idNV)
        {
            var hopDong = await LayHopDongMoiNhat(idNV);

            if (hopDong == null)
            {
                return Json(new { success = false, message = "Không tìm thấy hợp đồng." });
            }

            return Json(new
            {
                success = true,
                luongCoBan = hopDong.LuongCoBan.Value.ToString(),
                heSoLuong = hopDong.HeSoLuong.Value.ToString()
            });

        }
        private async Task<TienLuong?> GetTienLuongByIdAsync(int id)
        {
            return await _context.TienLuong
                .Include(tl => tl.NhanVien)
                .Include(tl => tl.HopDongLaoDong)
                .FirstOrDefaultAsync(tl => tl.IdTL == id);
        }
        // Phương thức lấy hợp đồng mới nhất của nhân viên
        private async Task<HopDongLaoDong?> LayHopDongMoiNhat(int idNV)
        {
            return await _context.HopDongLaoDong
                .Where(hd => hd.IdNV == idNV)
                .OrderByDescending(hd => hd.NgayBatDau)
                .FirstOrDefaultAsync();
        }
        private decimal TinhTongLuong(HopDongLaoDong? hopDong, TienLuong tienLuong)
        {
            decimal luongCoBan = hopDong?.LuongCoBan ?? 0;
            decimal heSoLuong = hopDong?.HeSoLuong ?? 0;
            return (luongCoBan * heSoLuong / 26 * tienLuong.SoNgayCong)
                   + (tienLuong.LuongTangCa ?? 0m)
                   - (tienLuong.LuongDaUng ?? 0m);
        }
        private async Task LoadRelatedData(TienLuong tienLuong)
        {
            tienLuong.NhanVien = await _context.NhanVien
                .FirstOrDefaultAsync(nv => nv.IdNV == tienLuong.IdNV);

            tienLuong.HopDongLaoDong = await _context.HopDongLaoDong
                .FirstOrDefaultAsync(hd => hd.IdHD == tienLuong.IdHD);
        }

    }
}
