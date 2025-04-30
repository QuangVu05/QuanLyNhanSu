# QuanLyNhanSu
Dự án xây dựng hệ thống Quản lý Nhân sự cho doanh nghiệp, giúp tự động hóa các quy trình:

Quản lý hồ sơ nhân viên

Quản lý hợp đồng lao động

Tính lương nhân viên

Khen thưởng và kỷ luật

Phân quyền người dùng theo vai trò

Xuất báo cáo dưới dạng Excel

Hỗ trợ đăng nhập và phân quyền

Ứng dụng được xây dựng theo mô hình MVC trên nền tảng ASP.NET Core, đảm bảo giao diện responsive và hệ thống vận hành ổn định trên nền web.

🛠️ Công nghệ sử dụng
Ngôn ngữ: C#

Framework: ASP.NET Core MVC

ORM: Entity Framework Core

Cơ sở dữ liệu: SQL Server

Frontend: Bootstrap 5, HTML5, CSS3

IDE: Visual Studio 2022

🔥 Các tính năng chính
Đăng nhập/Đăng xuất hệ thống

Phân quyền theo vai trò: Admin, Nhân sự, Kế toán

Quản lý nhân viên: thêm, sửa, xóa, tìm kiếm

Quản lý hợp đồng lao động

Tính lương nhân viên theo hệ số, ngày công

Ghi nhận khen thưởng và xử lý kỷ luật

Xuất báo cáo danh sách nhân viên ra file Excel

Giao diện responsive trên desktop và mobile

🖥️ Yêu cầu cài đặt
Visual Studio 2022 (hoặc mới hơn)

.NET SDK 6.0 trở lên

SQL Server (bản Express hoặc đầy đủ)

🚀 Hướng dẫn cài đặt và chạy dự án
Clone dự án về máy:

bash
Copy
Edit
git clone https://github.com/your-username/hr-management-system.git
Mở project bằng Visual Studio 2022.

Cấu hình chuỗi kết nối (connection string) trong file appsettings.json:

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=HRManagementDb;Trusted_Connection=True;"
}
Tạo database:

Mở Package Manager Console:

bash
Copy
Edit
Update-Database
Chạy ứng dụng (F5 hoặc Debug -> Start Debugging).

📸 Một số hình ảnh minh họa
(Bạn nên thêm ảnh giao diện ở đây ví dụ như: Trang đăng nhập, Dashboard quản lý nhân sự, Trang tính lương, Xuất Excel...)

📋 Thông tin liên hệ
Tác giả: [Tên bạn]

Email: [Email bạn]

LinkedIn (nếu có): [Link LinkedIn]

GitHub: [Link GitHub profile]
