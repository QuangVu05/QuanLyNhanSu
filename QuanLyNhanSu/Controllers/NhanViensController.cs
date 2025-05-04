using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.Helpers;
using System.Diagnostics;
using QuanLyNhanSu.Service_Layer;
using QuanLyNhanSu.ViewModels;
using ClosedXML.Excel;
using System.Net.Http;
using ClosedXML.Excel.Drawings;
using Microsoft.AspNetCore.Authorization;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using A = DocumentFormat.OpenXml.Drawing;
using WP = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml;








namespace QuanLyNhanSu.Controllers
{
    [Authorize(Roles = "Admin, HR Manager")]
    public class NhanViensController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        private readonly CountryService _countryService;
        private readonly HttpClient _httpClient;
        



        public NhanViensController(QuanLyNhanSuContext context, CountryService countryService)
        {
            _context = context;
            _countryService = countryService;
            _httpClient = new HttpClient();

        }

        private async Task SetDropdownLists()
        {
            // Lấy danh sách quốc gia từ API
            /*var countries = await _countryService.GetCountriesAsync();
            if (countries != null && countries.Any())
            {
                ViewData["QuocTich"] = new SelectList(countries);
            }*/
            var countries = _countryService.LoadFromFile();
            ViewData["QuocTich"] = new SelectList(countries);
            ViewData["IdBP"] = new SelectList(_context.BoPhan, "IdBP", "TenBP");
            ViewData["IdCV"] = new SelectList(_context.Set<ChucVu>(), "IdCV", "TenCV");
            ViewData["IdDT"] = new SelectList(_context.Set<DanToc>(), "IdDT", "TenDT");
            ViewData["IdPB"] = new SelectList(_context.Set<PhongBan>(), "IdPB", "TenPB");
            ViewData["IdTG"] = new SelectList(_context.Set<TonGiao>(), "IdTG", "TenTG");
            ViewData["IdTD"] = new SelectList(_context.Set<TrinhDo>(), "IdTD", "TenTD");
            ViewData["IdLoaiNV"] = new SelectList(_context.Set<LoaiNhanVien>(), "IdLoaiNV", "LoaiNV");
            ViewData["IdTTHonNhan"] = new SelectList(_context.Set<TTHonNhan>(), "IdTTHonNhan", "TTHonNhans");
            ViewData["IdTTLamViec"] = new SelectList(_context.Set<TTLamViec>(), "IdTTLamViec", "TTLamViecs");
            ViewData["IdGioiTinh"] = new SelectList(_context.Set<GioiTinh>(), "IdGioiTinh", "GioiTinhs");
        }

        
            public async Task<IActionResult> Index(string? chucVu, string? phongBan, string? trangThai)
        {
            var query = _context.NhanVien
                .Include(nv => nv.PhongBan)
                .Include(nv => nv.ChucVu)
                .Include(nv => nv.TTLamViec)
                .AsQueryable();

            if (!string.IsNullOrEmpty(chucVu))
            {
                query = query.Where(nv => nv.ChucVu.TenCV == chucVu);
            }

            if (!string.IsNullOrEmpty(phongBan))
            {
                query = query.Where(nv => nv.PhongBan.TenPB == phongBan);
            }

            if (!string.IsNullOrEmpty(trangThai))
            {
                query = query.Where(nv => nv.TTLamViec.TTLamViecs == trangThai);
            }

            var nhanVienList = await query.ToListAsync();

            var viewModel = nhanVienList.Select(nv => new NhanVienViewModel
            {
                IdNV = nv.IdNV,
                MaNV = nv.MaNV,
                Anh = nv.Anh,
                TenNV = nv.TenNV,
                SDT = nv.SDT,
                NgaySinh = nv.NgaySinh,
                QuocTich = nv.QuocTich,
                TenPB = nv.PhongBan?.TenPB,
                TenCV = nv.ChucVu?.TenCV,
                TTLamViecs = nv.TTLamViec?.TTLamViecs
            }).ToList();

            ViewBag.ChucVus = _context.ChucVu.Select(cv => cv.TenCV).Distinct().ToList();
            ViewBag.PhongBans = _context.PhongBan.Select(pb => pb.TenPB).Distinct().ToList();
            ViewBag.TrangThais = _context.TTLamViec.Select(tt => tt.TTLamViecs).Distinct().ToList();

            SetViewBagFilters(chucVu, phongBan, trangThai);
            return View(viewModel);
        }

        // GET: NhanViens/Details/5
        public async Task<IActionResult> Details(int? id, string? chucVu, string? phongBan, string? trangThai)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanVien
               .Include(n => n.BoPhan)
               .Include(n => n.ChucVu)
               .Include(n => n.DanToc)
               .Include(n => n.PhongBan)
               .Include(n => n.TonGiao)
               .Include(n => n.TrinhDo)
               .Include(n => n.LoaiNhanVien).Include(n => n.TTHonNhan).
               Include(n => n.TTLamViec).Include(n => n.GioiTinh)
               .FirstOrDefaultAsync(m => m.IdNV == id);
            if (nhanVien == null)
            {
                TempData["ErrorMessage"] = "Nhân viên không tồn tại.";

                return RedirectToAction(nameof(Index), new { chucVu, phongBan, trangThai });
            }
            SetViewBagFilters(chucVu, phongBan, trangThai);
            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public async Task<IActionResult> Create(string? chucVu, string? phongBan, string? trangThai)
        {
            /* // Lấy danh sách quốc gia từ API
             var countries = await _countryService.GetCountriesAsync();*/

            await SetDropdownLists();
            // Truyền danh sách quốc gia vào View
            SetViewBagFilters(chucVu, phongBan, trangThai);

            return View();
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNV,MaNV,Anh,TenNV,IdGioiTinh,CCCD,SDT,NgaySinh,QuocTich,NoiSinh,HoKhau,QueQuan,TamChu,IdLoaiNV,IdTTHonNhan,IdTTLamViec,IdBP,IdDT,IdPB,IdCV,IdTG,IdTD")] NhanVien nhanVien,
            IFormFile? anhFile)
        {
            var isCCCDDuplicate = await EntityHelper.CheckDuplicate(_context, _context.NhanVien,
                "CCCD", nhanVien.CCCD,
                "IdNV", null,
                "Căn cước công dân đã tồn tại trong hệ thống.", TempData);
            var isSDTDuplicate = await EntityHelper.CheckDuplicate(_context, _context.NhanVien,
                "SDT", nhanVien.SDT,
                "IdNV", null,
                "Số điện thoại đã tồn tại trong hệ thống", TempData);
            // Kiểm tra tuổi tối thiểu 18
            bool isUnderage = false;
            if (nhanVien.NgaySinh != null)
            {
                int tuoi = DateTime.Today.Year - nhanVien.NgaySinh.Value.Year;
                if (nhanVien.NgaySinh > DateTime.Today.AddYears(-tuoi))
                {
                    tuoi--; 
                }

                if (tuoi < 16)
                {
                    TempData["ErrorMessage"] = "Nhân viên phải từ 16 tuổi trở lên.";
                    isUnderage = true;
                }
            }

            // Nếu có lỗi, trả về form nhập lại
            if (isCCCDDuplicate || isSDTDuplicate || isUnderage)
                
            {
                // Nếu có ảnh mới được chọn, giữ lại đường dẫn tạm thời
                if (anhFile != null && anhFile.Length > 0)
                {
                    if (!KiemTraAnh(anhFile, out string imageError))
                    {
                        TempData["ErrorMessage"] = imageError;
                        await SetDropdownLists();
                        return View(nhanVien);
                    }
                    var tempPath = await LuuAnhTam(anhFile);
                    TempData["AnhMoi"] = tempPath;
                    nhanVien.Anh = tempPath;
                }

                await SetDropdownLists();

                return View(nhanVien);
            }


            if (ModelState.IsValid)
            {

                // Tạo mã nhân viên tự động với tiền tố "NV", đảm bảo không bị trùng lặp
                nhanVien.MaNV = await CodeGenerateHelper.GenerateCodeAsync(_context, _context.NhanVien, "MaNV", "NV");
                // Gọi hàm lưu ảnh
                if (anhFile != null && anhFile.Length > 0)
                {

                    string finalPath;
                    if (TempData.ContainsKey("AnhMoi"))
                    {
                        finalPath = DiChuyenAnhTuTamSangChinh(TempData["AnhMoi"].ToString());
                    }
                    else
                    {
                        finalPath = await LuuAnhNhanVien(anhFile);
                    }

                    nhanVien.Anh = finalPath;
                }
                try
                {
                    bool createSuccess = await EntityHelper.CreateEntity(_context, _context.NhanVien, nhanVien, "nhân viên", TempData);
                    if (createSuccess)
                    {

                        await SetDropdownLists();
                        return View(); // Hoặc trang danh sách nhân viên
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi cập nhật: {ex.Message}");
                    Console.WriteLine($"Lỗi cập nhật: {ex.Message}");
                    Console.WriteLine(ex.StackTrace); // Xem chi tiết lỗi
                    TempData["ErrorMessage"] = $"Lỗi xảy ra: {ex.Message}";
                }

            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Lỗi: {error.ErrorMessage}");
                }

                TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm nhân viên. Vui lòng thử lại.";
                await SetDropdownLists();
                return View(nhanVien);
            }
            // Hiển thị lại form nếu có lỗi
            TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm nhân viên. Vui lòng thử lại.";
            await SetDropdownLists();
            return View(nhanVien);


        }

        // GET: NhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id, string? chucVu, string? phongBan, string? trangThai)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanVien.FindAsync(id);
            if (nhanVien == null)
            {
                TempData["ErrorMessage"] = "Nhân viên không tồn tại.";

                return RedirectToAction(nameof(Index), new { chucVu, phongBan, trangThai });
            }


            await SetDropdownLists();
            SetViewBagFilters(chucVu, phongBan, trangThai);

            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNV,MaNV,Anh,TenNV,IdGioiTinh,CCCD," +
            "SDT,NgaySinh,QuocTich,NoiSinh,HoKhau,QueQuan,TamChu,IdLoaiNV,IdTTHonNhan,IdTTLamViec," +
            "IdBP,IdDT,IdPB,IdCV,IdTG,IdTD")] NhanVien nhanVien, IFormFile? anhFile, string? chucVu, string? phongBan, string? trangThai)
        {

            if (id != nhanVien.IdNV)
            {
                return NotFound();
            }
            var nhanVienCu = await _context.NhanVien.AsNoTracking().FirstOrDefaultAsync(nv => nv.IdNV == id);
            if (nhanVienCu == null)
            {
                return NotFound();
            }

            // Lưu ảnh cũ vào TempData để dùng lại nếu có lỗi
            TempData["AnhCu"] = nhanVienCu.Anh;
            var isCCCDDuplicate = await EntityHelper.CheckDuplicate(
                    _context,
                    _context.NhanVien,
                    "CCCD",
                    nhanVien.CCCD,
                    "IdNV",
                    id,
                    "Căn cước công dân đã tồn tại trong hệ thống.",
                    TempData
                );
            var isSDTDuplicate = await EntityHelper.CheckDuplicate(
                    _context,
                    _context.NhanVien,
                    "SDT",
                    nhanVien.SDT,
                    "IdNV",
                    id,
                    "Số điện thoại đã tồn tại trong hệ thống",
                    TempData
                );

            bool isUnderage = false;
            if (nhanVien.NgaySinh != null)
            {
                int tuoi = DateTime.Today.Year - nhanVien.NgaySinh.Value.Year;
                if (nhanVien.NgaySinh > DateTime.Today.AddYears(-tuoi))
                {
                    tuoi--;
                }

                if (tuoi < 16)
                {
                    TempData["ErrorMessage"] = "Nhân viên phải từ 16 tuổi trở lên.";
                    isUnderage = true;
                }
            }

            // Nếu có lỗi, trả về form nhập lại
            if (isCCCDDuplicate || isSDTDuplicate || isUnderage)
            {
                // Nếu có ảnh mới được chọn, giữ lại đường dẫn tạm thời
                if (anhFile != null && anhFile.Length > 0)
                {
                    
                    var tempPath = await LuuAnhTam(anhFile);
                    TempData["AnhMoi"] = tempPath;
                    TempData.Keep("AnhMoi");
                    nhanVien.Anh = tempPath;
                    
                }
                
                nhanVien.Anh = TempData["AnhMoi"] as string ?? TempData["AnhCu"] as string ?? nhanVien.Anh;
               

                TempData.Keep("AnhMoi");  // Giữ ảnh sau request
                await SetDropdownLists();
                return View(nhanVien);
            }



            if (ModelState.IsValid)
            {


                if (anhFile != null && anhFile.Length > 0)
                {
                    if (!KiemTraAnh(anhFile, out string imageError))
                    {
                        TempData["ErrorMessage"] = imageError;
                        nhanVien.Anh = TempData["AnhMoi"] as string ?? TempData["AnhCu"] as string ?? nhanVienCu.Anh;
                        TempData.Keep("AnhMoi");
                        await SetDropdownLists();
                        return View(nhanVien);
                    }
                    // Nếu có ảnh mới, xóa ảnh cũ trước rồi lưu ảnh mới
                    if (!string.IsNullOrEmpty(nhanVienCu.Anh))
                    {
                        XoaAnhNhanVien(nhanVienCu.Anh);
                    }
                    string finalPath = null;
                    if (TempData.ContainsKey("AnhMoi"))
                    {
                        finalPath = DiChuyenAnhTuTamSangChinh(TempData["AnhMoi"].ToString());
                    }
                    else
                    {
                        finalPath = await LuuAnhNhanVien(anhFile);
                    }

                    nhanVien.Anh = finalPath;
                }

                else
                {

                    nhanVien.Anh ??= nhanVienCu.Anh;

                }
                // Đặt trạng thái của entity thành Modified để cập nhật
                _context.Entry(nhanVien).State = EntityState.Modified;
                try
                {


                    bool isUpdated = await EntityHelper.EditEntity(_context, _context.NhanVien, nhanVien, "nhân viên", TempData);
                    if (isUpdated)
                    {
                        Console.WriteLine($"ChucVu: {nhanVien.ChucVu?.TenCV}");
                        Console.WriteLine($"PhongBan: {nhanVien.PhongBan?.TenPB}");
                        Console.WriteLine($"TrangThai: {nhanVien.TTLamViec?.TTLamViecs}");
                        await SetDropdownLists();
                        return RedirectToAction(nameof(Index), new { chucVu, phongBan, trangThai });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi cập nhật: {ex.Message}");
                }
            }

            // Hiển thị lại form nếu có lỗi
            TempData["ErrorMessage"] = "Có lỗi xảy ra khi sửa thông tin nhân viên. Vui lòng thử lại.";
            nhanVien.Anh = TempData.ContainsKey("AnhMoi") ? TempData["AnhMoi"] as string : TempData["AnhCu"] as string;

            await SetDropdownLists();
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public async Task<IActionResult> Delete(int id, string? chucVu, string? phongBan, string? trangThai)
        {
            var nhanVien = await _context.NhanVien.AsNoTracking().FirstOrDefaultAsync(nv => nv.IdNV == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            bool isDeleted = await EntityHelper.DeleteEntity(_context, _context.NhanVien, id, "Nhân viên", TempData);
            if (isDeleted && !string.IsNullOrEmpty(nhanVien.Anh))
            {

                XoaAnhNhanVien(nhanVien.Anh);

            }

            return RedirectToAction(nameof(Index), new { chucVu, phongBan, trangThai });
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = await _context.NhanVien.FindAsync(id);
            if (nhanVien != null)
            {
                _context.NhanVien.Remove(nhanVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
            return _context.NhanVien.Any(e => e.IdNV == id);
        }
        private void SetViewBagFilters(string? chucVu, string? phongBan, string? trangThai)
        {
            ViewBag.SelectedChucVu = chucVu;
            ViewBag.SelectedPhongBan = phongBan;
            ViewBag.SelectedTrangThai = trangThai;
        }
        private async Task<string> LuuAnhNhanVien(IFormFile anhFile)
        {
            if (anhFile == null || anhFile.Length == 0)
            {
                return null; // Ảnh mặc định nếu không có ảnh
            }

            try
            {
                var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(anhFile.FileName);
                var filePath = Path.Combine(imagesDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await anhFile.CopyToAsync(stream);
                }

                return "/images/" + fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu ảnh: " + ex.Message);
                return null;
            }
        }
        private void XoaAnhNhanVien(string? anhPath)
        {
            if (!string.IsNullOrEmpty(anhPath) && anhPath != "/images/anhDaiDien.png")
            {
                try
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", anhPath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi xóa ảnh: " + ex.Message);
                }
            }
        }
        private async Task<string> LuuAnhTam(IFormFile anhFile)
        {
            if (anhFile == null || anhFile.Length == 0)
            {
                return null;
            }

            // Thư mục lưu ảnh tạm
            string tempFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "temp");

            // Tạo thư mục nếu chưa có
            if (!Directory.Exists(tempFolder))
            {
                Directory.CreateDirectory(tempFolder);
            }

            // Tạo tên file ngẫu nhiên (VD: 87dc3f9a-abc1-4f8a.jpg)
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(anhFile.FileName)}";
            string filePath = Path.Combine(tempFolder, fileName);

            // Lưu file vào thư mục tạm
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await anhFile.CopyToAsync(stream);
            }

            // Trả về đường dẫn tương đối để hiển thị ảnh
            return $"/temp/{fileName}";
        }
        private string DiChuyenAnhTuTamSangChinh(string anhTam)
        {
            if (string.IsNullOrEmpty(anhTam))
            {
                return null;
            }

            // Định nghĩa đường dẫn thư mục
            string wwwRootPath = Directory.GetCurrentDirectory();
            string tempFolder = Path.Combine(wwwRootPath, "wwwroot", "temp");
            string uploadFolder = Path.Combine(wwwRootPath, "wwwroot", "images");

            // Tạo thư mục chính nếu chưa có
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Lấy tên file từ đường dẫn tạm
            string fileName = Path.GetFileName(anhTam);
            string tempFilePath = Path.Combine(tempFolder, fileName);
            string finalFilePath = Path.Combine(uploadFolder, fileName);

            // Kiểm tra xem file tạm có tồn tại không
            if (System.IO.File.Exists(tempFilePath))
            {
                // Di chuyển file sang thư mục chính
                System.IO.File.Move(tempFilePath, finalFilePath);

                // Trả về đường dẫn tương đối để lưu vào database
                return $"/images/{fileName}";
            }

            // Nếu không tìm thấy file tạm, trả về null
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (TempData["AnhMoi"] != null)
                {
                    // Nếu ảnh mới đã được lưu, không xóa
                    return;
                }
                XoaAnhTam(); // Chỉ xóa nếu không còn dùng nữa
            }
            base.Dispose(disposing);
        }
        private bool KiemTraAnh(IFormFile? file, out string errorMessage)
        {
            errorMessage = "";

            if (file == null || file.Length == 0)
            {
                errorMessage = "Không có file nào được tải lên.";
                return false;
            }

            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                errorMessage = "Chỉ được phép tải lên file ảnh (JPG, JPEG, PNG, GIF, BMP).";
                return false;
            }

            return true;
        }
        private void XoaAnhTam()
        {
            string tempFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "temp");

            if (Directory.Exists(tempFolder))
            {
                foreach (var file in Directory.GetFiles(tempFolder))
                {
                    try
                    {
                        System.IO.File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi xóa ảnh tạm: {ex.Message}");
                    }
                }
            }
        }
        [HttpGet]

        public async Task<IActionResult> ExportToExcel(string? chucVu, string? phongBan, string? trangThai)
        {
            var query = _context.NhanVien
                .Include(nv => nv.ChucVu)
                .Include(nv => nv.PhongBan)
                .Include(nv => nv.TTLamViec)
                .AsQueryable();

            // Lọc dữ liệu theo điều kiện
            if (!string.IsNullOrEmpty(chucVu))
            {
                query = query.Where(nv => nv.ChucVu.TenCV == chucVu);
            }
            if (!string.IsNullOrEmpty(phongBan))
            {
                query = query.Where(nv => nv.PhongBan.TenPB == phongBan);
            }
            if (!string.IsNullOrEmpty(trangThai))
            {
                query = query.Where(nv => nv.TTLamViec.TTLamViecs == trangThai);
            }

            var data = query.Select(nv => new
            {
                nv.MaNV,
                nv.Anh,
                nv.TenNV,
                nv.GioiTinh.GioiTinhs,
                nv.CCCD,
                nv.SDT,
                nv.NgaySinh,
                nv.QuocTich,
                nv.NoiSinh,
                nv.HoKhau,
                nv.QueQuan,
                nv.TamChu,
                nv.LoaiNhanVien.LoaiNV,
                nv.TTHonNhan.TTHonNhans,
                nv.TTLamViec.TTLamViecs,
                nv.BoPhan.TenBP,
                nv.DanToc.TenDT,
                nv.PhongBan.TenPB,
                nv.ChucVu.TenCV,
                nv.TonGiao.TenTG,
                nv.TrinhDo.TenTD,
            }).ToList();
            // Nếu danh sách trống, trả về thông báo lỗi
            if (data.Count == 0)
            {
                TempData["ErrorMessage"] = "Không có dữ liệu để xuất.";
               
                return RedirectToAction(nameof(Index), new { chucVu, phongBan, trangThai });
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Danh sách nhân viên");
                var currentRow = 1;
                // Danh sách tiêu đề cột
                var headers = new List<string>
                    {
                       "STT", "Mã NV", "Ảnh", "Tên nhân viên", "Giới tính", "CCCD", "SĐT", "Ngày sinh", "Quốc tịch",
                        "Nơi sinh", "Hộ khẩu", "Quê quán", "Tạm trú", "Loại nhân viên", "Tình trạng hôn nhân",
                        "Tình trạng làm việc", "Bộ phận", "Dân tộc", "Phòng ban", "Chức vụ", "Tôn giáo", "Trình độ"
                    };
                for (int i = 0; i < headers.Count; i++)
                {
                    worksheet.Cell(currentRow, i + 1).Value = headers[i];
                }


                // Định dạng tiêu đề
                var headerRow = worksheet.Range("A1:V1");
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                int index = 1;
                // Đổ dữ liệu vào Excel
                foreach (var nv in data)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = index++;
                    worksheet.Cell(currentRow, 2).Value = nv.MaNV ?? "N/A";
                    worksheet.Cell(currentRow, 4).Value = nv.TenNV ?? "N/A";
                    worksheet.Cell(currentRow, 5).Value = nv.GioiTinhs ?? "N/A";
                    worksheet.Cell(currentRow, 6).Value = nv.CCCD ?? "N/A";
                    worksheet.Cell(currentRow, 7).Value = nv.SDT ?? "N/A";
                    worksheet.Cell(currentRow, 8).Value = nv.NgaySinh?.ToString("dd/MM/yyyy") ?? "N/A";
                    worksheet.Cell(currentRow, 9).Value = nv.QuocTich ?? "N/A";
                    worksheet.Cell(currentRow, 10).Value = nv.NoiSinh ?? "N/A";
                    worksheet.Cell(currentRow, 11).Value = nv.HoKhau ?? "N/A";
                    worksheet.Cell(currentRow, 12).Value = nv.QueQuan ?? "N/A";
                    worksheet.Cell(currentRow, 13).Value = nv.TamChu ?? "N/A";
                    worksheet.Cell(currentRow, 14).Value = nv.LoaiNV ?? "N/A";
                    worksheet.Cell(currentRow, 15).Value = nv.TTHonNhans ?? "N/A";
                    worksheet.Cell(currentRow, 16).Value = nv.TTLamViecs ?? "N/A";
                    worksheet.Cell(currentRow, 17).Value = nv.TenBP ?? "N/A";
                    worksheet.Cell(currentRow, 18).Value = nv.TenDT ?? "N/A";
                    worksheet.Cell(currentRow, 19).Value = nv.TenPB ?? "N/A";
                    worksheet.Cell(currentRow, 20).Value = nv.TenCV ?? "N/A";
                    worksheet.Cell(currentRow, 21).Value = nv.TenTG ?? "N/A";
                    worksheet.Cell(currentRow, 22).Value = nv.TenTD ?? "N/A";

                    // Xử lý ảnh
                    if (!string.IsNullOrEmpty(nv.Anh))
                    {
                        try
                        {
                            string baseUrl = $"{Request.Scheme}://{Request.Host}";
                            string imageUrl = nv.Anh.StartsWith("http") ? nv.Anh : $"{baseUrl}{nv.Anh}";

                            var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
                           /* worksheet.Row(currentRow).Height = 60;  // Đặt chiều cao hàng lớn hơn ảnh
                            worksheet.Column(2).Width = 15;*/
                            using (var imageStream = new MemoryStream(imageBytes))
                            {
                                var picture = worksheet.AddPicture(imageStream)
                                                       .MoveTo(worksheet.Cell(currentRow, 3));
                                                       
                                picture.Width = 50;
                                picture.Height = 50;
                               worksheet.Row(currentRow).Height = picture.Height * 0.75; 
                               
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi tải ảnh: {ex.Message}");
                            worksheet.Cell(currentRow, 3).Value = "N/A"; // Hiển thị N/A nếu lỗi tải ảnh
                        }
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 3).Value = "N/A"; // Hiển thị N/A nếu không có ảnh
                    }

                }

                worksheet.Columns().AdjustToContents();
                // Xuất file
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    stream.Position = 0;
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachNhanVien.xlsx");
                }
            }
        }

        public async Task<IActionResult> ExportToWord(int id)
        {
            var nhanVienData = _context.NhanVien.Where(nv => nv.IdNV == id);

            if (nhanVienData == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy nhân viên.";
                return RedirectToAction(nameof(Index));
            }

            var nhanVien = await nhanVienData
                .Select(nv => new
                {
                    nv.MaNV,
                    nv.Anh,
                    nv.TenNV,
                    GioiTinh = nv.GioiTinh.GioiTinhs,
                    nv.CCCD,
                    nv.SDT,
                    nv.NgaySinh,
                    nv.QuocTich,
                    nv.NoiSinh,
                    nv.HoKhau,
                    nv.QueQuan,
                    nv.TamChu,
                    LoaiNhanVien = nv.LoaiNhanVien.LoaiNV,
                    TTHonNhan = nv.TTHonNhan.TTHonNhans,
                    TTLamViec = nv.TTLamViec.TTLamViecs,
                    BoPhan = nv.BoPhan.TenBP,
                    DanToc = nv.DanToc.TenDT,
                    PhongBan = nv.PhongBan.TenPB,
                    ChucVu = nv.ChucVu.TenCV,
                    TonGiao = nv.TonGiao.TenTG,
                    TrinhDo = nv.TrinhDo.TenTD,
                })
                .FirstOrDefaultAsync();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(memoryStream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                    mainPart.Document = new Document(new Body());

                    // Thêm tiêu đề
                    Paragraph title = new Paragraph(new Run(new Text("Thông tin nhân viên")))
                    {
                        ParagraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center })
                    };
                    mainPart.Document.Body.AppendChild(title);

                   

                    // Thêm thông tin nhân viên vào bảng
                    Table table = new Table();
                    table.AppendChild(new TableProperties(new TableStyle() { Val = "TableGrid" }));

                    void AddRow(string label, string value)
                    {
                        TableRow row = new TableRow();
                        row.AppendChild(new TableCell(new Paragraph(new Run(new Text(label)))));
                        row.AppendChild(new TableCell(new Paragraph(new Run(new Text(value ?? "N/A")))));
                        table.AppendChild(row);
                    }

                    AddRow("Mã nhân viên:", nhanVien.MaNV.ToString());
                    AddRow("Tên nhân viên:", nhanVien.TenNV);
                    AddRow("Giới tính:", nhanVien.GioiTinh);
                    AddRow("CCCD:", nhanVien.CCCD);
                    AddRow("SĐT:", nhanVien.SDT);
                    AddRow("Ngày sinh:", nhanVien.NgaySinh?.ToString("dd/MM/yyyy"));
                    AddRow("Quốc tịch:", nhanVien.QuocTich);
                    AddRow("Nơi sinh:", nhanVien.NoiSinh);
                    AddRow("Hộ khẩu:", nhanVien.HoKhau);
                    AddRow("Quê quán:", nhanVien.QueQuan);
                    AddRow("Tạm trú:", nhanVien.TamChu);
                    AddRow("Loại nhân viên:", nhanVien.LoaiNhanVien);
                    AddRow("Tình trạng hôn nhân:", nhanVien.TTHonNhan);
                    AddRow("Tình trạng làm việc:", nhanVien.TTLamViec);
                    AddRow("Bộ phận:", nhanVien.BoPhan);
                    AddRow("Dân tộc:", nhanVien.DanToc);
                    AddRow("Phòng ban:", nhanVien.PhongBan);
                    AddRow("Chức vụ:", nhanVien.ChucVu);
                    AddRow("Tôn giáo:", nhanVien.TonGiao);
                    AddRow("Trình độ:", nhanVien.TrinhDo);
                    // Thêm đường link ảnh nhân viên nếu có
                    // Thêm đường link ảnh nhân viên nếu có
                    if (!string.IsNullOrEmpty(nhanVien.Anh))
                    {
                        string imageUrl = nhanVien.Anh.StartsWith("http") ? nhanVien.Anh : $"{Request.Scheme}://{Request.Host}{nhanVien.Anh}";

                        AddRow("Ảnh nhân viên:", imageUrl); // Hiển thị URL ảnh trong bảng

                        // Tạo Hyperlink có thể click
                        HyperlinkRelationship hyperlinkRel = mainPart.AddHyperlinkRelationship(new Uri(imageUrl), true);
                        Hyperlink hyperlink = new Hyperlink(new Run(new Text("Xem ảnh nhân viên")))
                        {
                            Id = hyperlinkRel.Id
                        };

                        Paragraph linkParagraph = new Paragraph(hyperlink);
                        mainPart.Document.Body.AppendChild(linkParagraph);
                    }

                    mainPart.Document.Body.AppendChild(table);
                }
                // 📌 Thêm ảnh nếu có
               

            memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"NhanVien_{nhanVien.MaNV}.docx");
            }
        }

    }

}


