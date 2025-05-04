using Microsoft.AspNetCore.Mvc;

namespace QuanLyNhanSu.Helpers
{
    public static class ModelStateHelper
    {
        public static void CheckFieldError(Controller controller, string fieldName, string errorMessage)
        {
            if (controller.ModelState.ContainsKey(fieldName))
            {
                var fieldErrors = controller.ModelState[fieldName].Errors;

                // Kiểm tra nếu có lỗi tương ứng
                if (fieldErrors.Any(e => e.ErrorMessage == errorMessage))
                {
                    controller.TempData[$"{fieldName}Error"] = errorMessage;
                }
            }
        }

    }
}
