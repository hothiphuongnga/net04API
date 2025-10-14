using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class NhaCungCap
{
    public int Id { get; set; }

    public string? Ten { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<NhapHang> NhapHangs { get; set; } = new List<NhapHang>();
}
