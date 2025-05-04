using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Helpers
{
    public class EntityHelper
    {
        public static async Task<bool> CheckDuplicate<T>(
          QuanLyNhanSuContext context,
          DbSet<T> dbSet,
          string columnName,
          string value,
          string primaryKeyColumn, // Thêm tham số cho khóa chính
          int? id,
          string errorMessage,
          ITempDataDictionary tempData) where T : class
            {
            bool isDuplicate;
            if (id == null)
            {
                // THÊM MỚI: chỉ kiểm tra trùng lặp
                isDuplicate = await dbSet.AnyAsync(e => EF.Property<string>(e, columnName) == value);
            }
            else
            {
                // CHỈNH SỬA: loại trừ bản ghi đang sửa
                isDuplicate = await dbSet.AnyAsync(e =>
                    EF.Property<string>(e, columnName) == value &&
                    EF.Property<int>(e, primaryKeyColumn) != id
                );
            }
            if (isDuplicate)
                {
                    tempData["ErrorMessage"] = errorMessage;
                    return true;
                }
                return false;
            }
        // Kiểm tra trùng lặp và xử lý thông báo lỗi

        public static void ValidateModelErrors(
            ModelStateDictionary modelState,
            string columnName,
            ITempDataDictionary tempData,
            string requiredMessage = "Trường này là bắt buộc.",
            string maxLengthMessage = "Độ dài vượt quá giới hạn."
        )
        {
            if (modelState.ContainsKey(columnName))
            {
                var errors = modelState[columnName].Errors;

                if (errors.Any(e => e.ErrorMessage.Contains("bắt buộc")))
                {
                    tempData[$"{columnName}Error"] = requiredMessage;
                }
                else if (errors.Any(e => e.ErrorMessage.Contains("vượt quá")))
                {
                    tempData[$"{columnName}Error"] = maxLengthMessage;
                }
            }
            else
            {
                tempData[$"{columnName}Error"] = null;
            }
        }
        // Thêm đối tượng v DB
        public static async Task<bool> CreateEntity<T>(
            QuanLyNhanSuContext context,
            DbSet<T> dbSet,
            T entity,
            string entityName,
            ITempDataDictionary tempData
        ) where T : class
        {
            
            try
            {
                dbSet.Add(entity);
                await context.SaveChangesAsync();
                tempData["SuccessMessage"] = $"Thêm {entityName} thành công!";
                return true;
            }
            catch (Exception)
            {
                tempData["ErrorMessage"] = $"Có lỗi xảy ra khi thêm {entityName}.";
                return false;
            }
        }
        // Cập nhật đối tượng trong DB
        public static async Task<bool> EditEntity<T>(
            QuanLyNhanSuContext context,
            DbSet<T> dbSet,
            T entity,
            string entityName,
            ITempDataDictionary tempData
        ) where T : class
        {

            try
            {
                

                // Gán giá trị từ entity mới vào thực thể hiện có
               // context.Entry(entity).State = EntityState.Modified;
                 dbSet.Update(entity);
                await context.SaveChangesAsync();
                tempData["SuccessMessage"] = $"Cập nhật {entityName} thành công!";
                return true;
            }
            catch (Exception ex)
            {
                tempData["ErrorMessage"] = $"Có lỗi xảy ra khi cập nhật {entityName}.";
                Console.WriteLine($"Lỗi khi cập nhật {entityName}: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                tempData["ErrorMessage"] = $"Lỗi: {ex.Message}";
                return false;
            }
        }

        // Xóa đối tượng khỏi DB
        public static async Task<bool> DeleteEntity<T>(
            QuanLyNhanSuContext context,
            DbSet<T> dbSet,
            int id,
            string entityName,
            ITempDataDictionary tempData
        ) where T : class
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                tempData["ErrorMessage"] = $"{entityName} không tồn tại!";
                return false;
            }

            try
            {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
                tempData["SuccessMessage"] = $"{entityName} đã được xóa thành công!";
                return true;
            }
            catch (Exception)
            {
                tempData["ErrorMessage"] = $"Có lỗi xảy ra khi xóa {entityName}.";
                return false;
            }
        }
    }
}

