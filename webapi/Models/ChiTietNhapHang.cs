using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class ChiTietNhapHang
{
    public int MaSanPham { get; set; }

    public int MaNhapHang { get; set; }

    public int? SoLuong { get; set; }

    public decimal? Gia { get; set; }

    public virtual NhapHang MaNhapHangNavigation { get; set; } = null!;

    public virtual SanPham MaSanPhamNavigation { get; set; } = null!;
}
