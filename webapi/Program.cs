using Microsoft.EntityFrameworkCore;
using webapi.Data;

var builder = WebApplication.CreateBuilder(args);

//map cái controller có gắn [Route] vào hệ thống
//tự động tìm các controller trong dự án
//
builder.Services.AddControllers();


// đăng ký services để sử dụng swagger
// tạo document dựa trên các api controller
// đường đẫn mặc định : /swagger/v1/swagger.json
builder.Services.AddSwaggerGen();

// thêm AutoMapper
// builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());
builder.Services.AddAutoMapper(cfg=> { },typeof(AutoMapperProfile));

// cấu hình kết nối db
// Thêm Entity Framework 
builder.Services.AddDbContext<DataContext>(options =>
{
    // lấy chuỗi kết nối từ appsettings.json
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

// thêm dbcontext của project QuanLyBanHang
// để tương tác với db QuanLyBanHang 
builder.Services.AddDbContext<QuanLyBanHangContext>(options =>
{
    // lấy chuỗi kết nối từ appsettings.json
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});



var app = builder.Build();
// phân biệt môi trường dev(local) và prod (deploy lên host)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();// map các controller có gắn [Route] vào hệ thống
app.UseSwagger();// http://localhost:5001/swagger/v1/swagger.json
app.UseSwaggerUI();// http://localhost:5001/swagger/index.html


// app.UseHttpsRedirection();

app.Run();


//REST API
//HTTP GET POST PUT DELETE
//



// RESTFULL API
// Post thêm mới
// PUt sửa 
// dung put để thêm sp và sai quy tắc của restfull


//ROUTE : 
//api/nguoidung/....
// api/sanpham 