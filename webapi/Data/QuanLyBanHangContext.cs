using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data;

public partial class QuanLyBanHangContext : DbContext
{
    public QuanLyBanHangContext()
    {
    }

    public QuanLyBanHangContext(DbContextOptions<QuanLyBanHangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietNhapHang> ChiTietNhapHangs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<DonHangSanPham> DonHangSanPhams { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<NhapHang> NhapHangs { get; set; }

    public virtual DbSet<PhanLoaiSanPham> PhanLoaiSanPhams { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietNhapHang>(entity =>
        {
            entity.HasKey(e => new { e.MaSanPham, e.MaNhapHang }).HasName("PK__ChiTietN__4EE98FF34909D01E");

            entity.ToTable("ChiTietNhapHang");

            entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MaNhapHangNavigation).WithMany(p => p.ChiTietNhapHangs)
                .HasForeignKey(d => d.MaNhapHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTNH_NH");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietNhapHangs)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTNH_SP");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DonHang__3214EC070C83AAA4");

            entity.ToTable("DonHang");

            entity.Property(e => e.NgayMua)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_DONHANG_KHACHHANG");
        });

        modelBuilder.Entity<DonHangSanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DonHang___3214EC07AE81A4E3");

            entity.ToTable("DonHang_SanPham");

            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.DonHangSanPhams)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK_DonHangSP_DonHang");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.DonHangSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK_DonHangSP_SanPham");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KhachHan__3214EC071B7F013D");

            entity.ToTable("KhachHang");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Sdt).HasMaxLength(20);
            entity.Property(e => e.Ten).HasMaxLength(100);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NhaCungC__3214EC07A9C9216E");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Stk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STK");
            entity.Property(e => e.Ten).HasMaxLength(255);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NhanVien__3214EC07C5CFA791");

            entity
                .ToTable("NhanVien")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("NhanVien_History", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Luong)
                .HasDefaultValue(5100000m)
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Sdt).HasMaxLength(20);
            entity.Property(e => e.Ten).HasMaxLength(100);

            entity.HasOne(d => d.MaQuanLyNavigation).WithMany(p => p.InverseMaQuanLyNavigation)
                .HasForeignKey(d => d.MaQuanLy)
                .HasConstraintName("FK_NhanVien_Parent");
        });

        modelBuilder.Entity<NhapHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NhapHang__3214EC073CFF91AA");

            entity.ToTable("NhapHang");

            entity.Property(e => e.MaNcc).HasColumnName("MaNCC");
            entity.Property(e => e.NgayNhaphang)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.NhapHangs)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK_NhapHang_NCC");
        });

        modelBuilder.Entity<PhanLoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhanLoai__3214EC07A0155AF0");

            entity.ToTable("PhanLoaiSanPham");

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenPhanLoai).HasMaxLength(100);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_PhanLoai_Parent");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SanPham__3214EC07D35792B7");

            entity.ToTable("SanPham");

            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Ten).HasMaxLength(100);

            entity.HasOne(d => d.MaPhanLoaiNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaPhanLoai)
                .HasConstraintName("FK_SanPham_PhanLoai");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__test__3214EC0744298810");

            entity.ToTable("test");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
