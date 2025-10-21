using System.ComponentModel.DataAnnotations;

namespace webapi.ViewModels;

public class KhachHangVM
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên không được để trống")]
    public string? Ten { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [RegularExpression(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Email không hợp lệ")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    public string? Sdt { get; set; }
    // public List<string> MetaData { get; set; } = new List<string>();

}