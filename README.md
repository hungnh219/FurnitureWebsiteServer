
# Furniture Website (Server-Side)
Trang web dùng để bán thiết bị nội thất

Dùng làm đồ án cuối kỳ môn **Internet và Công nghệ Web** - *(E104.O12 - Kỳ 1 2023 - UIT)*

## Yêu Cầu
- Sử dụng IDE: **Visual Studio** *(Nên tải phiên bản 2022 để đảm bảo cài đặt .NET 7.0)*
- Tải file **MSSQL.sql** trong folder data tại client: https://github.com/hungnh219/FurnitureWebsite

## Hướng dẫn
### Add dependencies
  - Microsoft Entity Framework Core - 7.0
  - Microsoft Entity Framework Core SQLServer - 7.0
  - Microsoft Entity Framework Core Tools - 7.0
### Connect database EF - Database First
  - Chạy file SQL trước (nên sử dụng SSMS)
  - Connect database với Visual Studio
### Tạo Entities Model
  - Mở Package Manage Console
  - Connect database với Visual Studio
  - Tạo folder Entities
```
Scaffold-DbContext <Connection String của db> Microsoft.EntityFrameworkCore.SqlSever -OutputDir <tên folder entities>
```
  - Chọn database - Chọn Properties để lấy Connection String
  - Nếu xảy ra lỗi thêm đoạn code vào cuối Connection String: TrustServerCertificate=True
  - Tạo chuỗi kết nối trong appsettings.json
 ```     
"ConnectionStrings": {
    "<Ten db>": "Server=.;Database=<Ten db>;Integrated 	Security=True;TrustServerCertificate=True"
  }
```
  - add DbContext trong program.cs
```
builder.Services.AddDbContext<QuanLySinhVienContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("QuanLySinhVien")));
```

***Khi này, server đã được setup xong***
**Có thể Buil và Run để tạo các API Endpoint để cung cấp dữ liệu và đảm một mộ số chức năng cho trang web như:**\
      - Đăng nhập\
      - Sửa thông tin\
      - Lấy dữ liệu sản phẩm\
      - Đổi mật khẩu\
      - Đăng ký\
      ...
      
### Góp ý
hungnh219@gmail.com 
