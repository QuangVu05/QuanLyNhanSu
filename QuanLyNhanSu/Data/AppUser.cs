using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Models;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanSu.Data
{
    public class AppUser : IdentityUser
    {
        
       
        public virtual NhanVien? NhanVien { get; set; }
    }
}
