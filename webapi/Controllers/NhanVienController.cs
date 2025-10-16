
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
namespace webapi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class NhanVienController : ControllerBase
{
    // inject DataContext vào controller để tương tác với DB
    // private readonly DataContext _context;
    // đổi sang dung QuanLyBanHangContext
    private readonly QuanLyBanHangContext _context;

    // public NhanVienController(DataContext context)
    public NhanVienController(QuanLyBanHangContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await _context.NhanViens.ToListAsync();
        return new ResponseEntity(200, res, "Lấy danh sách nhân viên thành công");
    }
}
// scaffold để update 1 table 
