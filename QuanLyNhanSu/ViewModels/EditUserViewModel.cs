using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.ViewModels
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập tối đa 50 ký tự")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Vai trò không được để trống")]
        public string? Role { get; set; }
    }
}
