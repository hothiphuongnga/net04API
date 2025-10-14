using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class KhachHang
{
    public int Id { get; set; }

    public string? Ten { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
