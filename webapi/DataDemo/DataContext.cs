using Microsoft.EntityFrameworkCore;
using webapi.ModelsDemo;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    // Thêm dbset cho các entity vào đây
    // muốn làm việc với table NhanVien trong db 
    public DbSet<NhanVien> NhanVien { get; set; }
    // khai báo các bảng trong db
    // bảng SanPham
    public DbSet<SanPham> SanPham { get; set; }

// cấu hình các ràng buộc, khoá ngoại, tên bảng, tên cột ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // cấu hình cho bảng NhanVien


        // cấu hình cho bảng SanPham

    }
}