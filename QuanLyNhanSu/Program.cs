using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Service_Layer;

var builder = WebApplication.CreateBuilder(args);
 builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<QuanLyNhanSuContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyNhanSuContext") ?? throw new InvalidOperationException("Connection string 'QuanLyNhanSuContext' not found.")));

builder.Services.AddHttpClient(); // Thêm IHttpClientFactory vào container DI
// Đăng ký Service Layer
builder.Services.AddHostedService<TempImageCleanupService>();
builder.Services.AddSingleton<CountryService>(); // Đăng ký service 
builder.Services.AddHostedService(provider => provider.GetRequiredService<CountryService>());

/*builder.Services.AddTransient<CountryService>();*/

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<QuanLyNhanSuContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false; // Không bắt buộc số
    options.Password.RequiredLength = 6; // Độ dài tối thiểu 6 ký tự
    options.Password.RequireNonAlphanumeric = false; // Không bắt buộc ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ hoa
    options.Password.RequireLowercase = false; // Không bắt buộc chữ thường
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(30); // Nếu RememberMe = true
    options.SlidingExpiration = true;
    options.LoginPath = "/Account/Login";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
