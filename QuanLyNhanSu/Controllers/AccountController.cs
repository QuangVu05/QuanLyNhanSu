using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.ViewModels;

namespace QuanLyNhanSu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; 

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email không tồn tại.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Mật khẩu không đúng.");
                return View(model);
            }
            TempData["SuccessMessage"] = "Đăng nhập thành công.";
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại."); // Thông báo lỗi nếu email đã có người dùng khác
                return View(model);

            }

            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName=model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
               

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(model.Role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(model.Role));
                    }

                    await _userManager.AddToRoleAsync(user, model.Role);

                    TempData["SuccessMessage"] = "Tạo tài khoản thành công!";
                    return RedirectToAction("UserList");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            TempData["ErrorMessage"] = "Tạo tài khoản thất bại. Vui lòng kiểm tra lại thông tin!";
            return View(model);
        }
       
        [HttpGet]
        public async Task< IActionResult> UserList()
        {
            var users = _userManager.Users.ToList();
            var userRoles = new Dictionary<string, List<string>>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles.ToList();
            }

            ViewBag.UserRoles = userRoles; // Gán dữ liệu đúng kiểu

            return View(users);
        }
        /* public IActionResult ChangePassword() => PartialView("_ChangePassword");
         [HttpPost]
         public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
         {
             if (!ModelState.IsValid)
             {
                 return View(model); // Nếu có lỗi thì hiển thị lại form
             }

             var user = await _userManager.FindByEmailAsync(model.Email);
             if (user == null)
             {
                 ModelState.AddModelError("Email", "Email không tồn tại!");
                 return View(model);
             }

             var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
             var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

             if (result.Succeeded)
             {
                 TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                 return RedirectToAction("Login");
             }

             foreach (var error in result.Errors)
             {
                 ModelState.AddModelError("", error.Description);
             }

             return View(model);
         }*/
        
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.Any() ? roles[0] : "User";

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Role = role
            };

            return PartialView("_EditUser", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditUser", model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            try
            {
                user.UserName = model.UserName;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    TempData["ErrorMessage"] = "Cập nhật thất bại!";
                    return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
                }

                // Cập nhật vai trò
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, model.Role);

                TempData["SuccessMessage"] = "Cập nhật thành công!";
                return Json(new { success = true, successMessage = TempData["SuccessMessage"] });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return Json(new { success = false, errorMessage = $"Lỗi xảy ra: {ex.Message}" });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng.";
                return RedirectToAction("UserList");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Xóa người dùng thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Xóa thất bại. Vui lòng thử lại.";
            }

            return RedirectToAction("UserList");
        }
       
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}

