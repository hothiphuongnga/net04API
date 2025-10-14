using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class DonHang
{
    public int Id { get; set; }

    public DateTime? NgayMua { get; set; }

    public decimal? TongTien { get; set; }

    public int? MaKhachHang { get; set; }

    public virtual ICollection<DonHangSanPham> DonHangSanPhams { get; set; } = new List<DonHangSanPham>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }
}
