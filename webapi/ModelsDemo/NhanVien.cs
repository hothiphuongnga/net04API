namespace webapi.ModelsDemo;
public class NhanVien
{
    public int Id { get; set; }
    public string Ten { get; set; }
    public string Email { get; set; }
    public string Sdt { get; set; }
    public decimal Luong { get; set; }
    public int? MaQuanLy { get; set; } =null; // Nullable<int>
}