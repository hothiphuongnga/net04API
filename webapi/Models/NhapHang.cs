using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class NhapHang
{
    public int Id { get; set; }

    public DateTime? NgayNhaphang { get; set; }

    public decimal? TongTien { get; set; }

    public int? MaNcc { get; set; }

    public virtual ICollection<ChiTietNhapHang> ChiTietNhapHangs { get; set; } = new List<ChiTietNhapHang>();

    public virtual NhaCungCap? MaNccNavigation { get; set; }
}
