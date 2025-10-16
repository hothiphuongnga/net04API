using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapi.Modeltemp;

namespace webapi.Dattempa;

public partial class QuanLyBanHangContext : DbContext
{
    public QuanLyBanHangContext()
    {
    }

    public QuanLyBanHangContext(DbContextOptions<QuanLyBanHangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
