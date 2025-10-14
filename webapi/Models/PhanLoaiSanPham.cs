using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class PhanLoaiSanPham
{
    public int Id { get; set; }

    public string? TenPhanLoai { get; set; }

    public string? MoTa { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<PhanLoaiSanPham> InverseParent { get; set; } = new List<PhanLoaiSanPham>();

    public virtual PhanLoaiSanPham? Parent { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
