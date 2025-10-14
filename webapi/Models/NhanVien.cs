using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace webapi.Models;

public partial class NhanVien
{
    public int Id { get; set; }

    public string? Ten { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public decimal? Luong { get; set; }

    public int? MaQuanLy { get; set; } = null;

    [JsonIgnore]

    public virtual ICollection<NhanVien> InverseMaQuanLyNavigation { get; set; } = new List<NhanVien>();

    [JsonIgnore]

    public virtual NhanVien? MaQuanLyNavigation { get; set; }
}
