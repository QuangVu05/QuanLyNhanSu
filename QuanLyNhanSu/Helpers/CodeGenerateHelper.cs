using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Helpers
{
    public  class CodeGenerateHelper
    {
        public static async Task<string> GenerateCodeAsync<T>(QuanLyNhanSuContext context, DbSet<T> dbSet, string columnName, string prefix) where T : class
        {
            // Lấy hai chữ số cuối của năm hiện tại
            string yearPart = DateTime.Now.Year.ToString().Substring(2, 2); // Lấy 2 chữ số cuối của năm

            // Tạo mã ngẫu nhiên với 4 chữ số
            Random rand = new Random();
            string randomPart = Guid.NewGuid().ToString("N").Substring(0, 4); // Lấy 4 chữ số ngẫu nhiên

            // Tạo mã với cấu trúc: "Tự dựng" + 2 chữ số cuối năm + 4 chữ số ngẫu nhiên
            string newCode = $"{prefix}{yearPart}{randomPart}";

            // Kiểm tra xem mã đã tồn tại trong cơ sở dữ liệu chưa
            while (await CheckDuplicate(context, dbSet, columnName, newCode))
            {
                // Nếu mã trùng lặp, tạo lại mã ngẫu nhiên mới
                randomPart = Guid.NewGuid().ToString("N").Substring(0, 4);
                newCode = $"{prefix}{yearPart}{randomPart}";
            }

            return newCode;
        }

        // Kiểm tra mã có bị trùng lặp trong cơ sở dữ liệu không
        private static async Task<bool> CheckDuplicate<T>(QuanLyNhanSuContext context, DbSet<T> dbSet, string columnName, string value) where T : class
        {
            var existingEntity = await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => EF.Property<string>(e, columnName) == value);

            return existingEntity != null;
        }
    }
}
