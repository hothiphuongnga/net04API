using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class SanPham
{
    public int Id { get; set; }

    public string? Ten { get; set; }

    public decimal? Gia { get; set; }

    public int? MaPhanLoai { get; set; }

    public virtual ICollection<ChiTietNhapHang> ChiTietNhapHangs { get; set; } = new List<ChiTietNhapHang>();

    public virtual ICollection<DonHangSanPham> DonHangSanPhams { get; set; } = new List<DonHangSanPham>();

    public virtual PhanLoaiSanPham? MaPhanLoaiNavigation { get; set; }
}
