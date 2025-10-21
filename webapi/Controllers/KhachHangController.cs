using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using webapi.ViewModels;
namespace webapi.Controllers;



[Route("api/[controller]")]
[ApiController]
public class KhachHangController : ControllerBase
{
    // inject QuanLyBanHangContext vào controller để tương tác với DB
    private readonly QuanLyBanHangContext _context;
    private readonly IMapper _mapper;
    public KhachHangController(QuanLyBanHangContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // lấy danh sách khách hàng từ db
        // bao gồm cả danh sách đơn hàng của khách hàng
        var res = await _context.KhachHangs
        .Include(k => k.DonHangs)
        .ToListAsync();
        // => Select * from KhachHang
        return new ResponseEntity(200, res, "Lấy danh sách khách hàng thành công");
    }

    //lấy ra 1 khách hàng theo id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // bổ sung để lấy ra cả danh sách đơn hàng của khách hàng
        var res = await _context.KhachHangs
        .Include(k => k.DonHangs)
        .FirstOrDefaultAsync(p => p.Id == id);
        // => Select * from KhachHang where Id = id
        if (res == null)
            return new ResponseEntity(404, null, "Không tìm thấy khách hàng với id = " + id);

        return new ResponseEntity(200, res, "Lấy khách hàng theo id thành công");
    }
    // cập nhật khách hàng
    // xóa khách hàng
    // thêm mới khách hàng
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] KhachHangVM khachHang)
    {
        // xử lý theem dữ liệu vaf db
        // kieerm tra dữ liệu đầu vào
        // ModelState : 
        if (!ModelState.IsValid)
        {
            // trả về lỗi 400 bad request
            return new ResponseEntity(400, ModelState, "Dữ liệu không hợp lệ");
        }
        
        KhachHang kh = _mapper.Map<KhachHang>(khachHang);
        // <KhachHang> là kiểu trả về mong muốn
        // (khachHang) là cái cần chuyển




        // KhachHang kh = new KhachHang();
        // kh.Id = 0; // thêm mới thì id = 0
        // kh.Ten = khachHang.Ten;
        // kh.Email = khachHang.Email;
        // kh.Sdt = khachHang.Sdt;

        // xử lý đúng thêm mới
        // EF
        // 
        _context.KhachHangs.Add(kh); // "mới commit chưa push"
        // chỉ mởi đánh đấu thêm mới
        // chuaw xong
        // kết quả của SaveChangesAsync là số bản ghi bị ảnh hưởng
        int res = await _context.SaveChangesAsync(); // "đã push lên db"
        // thực hiện câu lệnh insert into
        return new ResponseEntity(201, res, "Thêm mới khách hàng thành công");
    }

    // cập nhật khách hàng
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] KhachHangVM khachHang)
    {
        // kiểm tra dữ liệu đầu vào
        if (!ModelState.IsValid)
        {
            return new ResponseEntity(400, ModelState, "Dữ liệu không hợp lệ");
        }
        // check id trùng không
        if (id != khachHang.Id)
        {
            return new ResponseEntity(400, null, "Id không trùng khớp");
        }

        // tìm khách hàng có id tương ứng
        // dùng Find nhanh hơn firstordèault vì tìm trưc tiếp = index
        KhachHang find = await _context.KhachHangs.FindAsync(id);
        if (find == null)
        {
            return new ResponseEntity(404, find, "Không tìm thấy");
        }
        // chuyển KhachHangVM -> KhachHang
        // thêm 10 field 
        find.Email = khachHang.Email;
        find.Sdt = khachHang.Sdt;
        find.Ten = khachHang.Ten;
        // cập nhật khách hàng
        // thêm cho EF biết là cập nhật
        // không cần thiết vì EF tự động tracking
        _context.KhachHangs.Update(find);
        // EF tự động tracking khachhang thây đỏi để update database
        int res = await _context.SaveChangesAsync();
        return new ResponseEntity(200, res, "Cập nhật khách hàng thành công");
    }

    // xoá khách hàng
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // tìm khách hàng có id tương ứng
        var find = await _context.KhachHangs
        .Include(k => k.DonHangs)
        .FirstOrDefaultAsync(k => k.Id == id);
        if (find == null)
        {
            return new ResponseEntity(404, null, "Không tìm thấy khách hàng với id = " + id);
        }
        // kiểm tra khách hàng có đơn hàng chưa
        // kiểm tra DonHangs trong find
        // if (find.DonHangs.Count() != 0)
        // {
        //     return new ResponseEntity(400, 0, "Không thể xoá khách hàng đang có đơn");
        // }

        // cách khác để ktra ràng buộc
        // vào đơn hàng tìm xem có đơn nào mang mã khách hàng này hay không
        var checkDonHang = await _context.DonHangs.FirstOrDefaultAsync(d => d.MaKhachHang == id);

        // == null không có đơn => xoá
        // != null thì không xoá => 400
        if (checkDonHang != null)
        {
            return new ResponseEntity(400, 0, "Không thể xoá khách hàng đang có đơn");
        }
        // xoá khách hàng
        _context.KhachHangs.Remove(find);
        // gọi save change
        int res = await _context.SaveChangesAsync();
        return new ResponseEntity(200, res, "Xoá khách hàng thành công");
    }


    // 
    // 
    // dùng SQL RAW
    // không nên dùng
    [HttpPost("sql")]
    public async Task<IActionResult> CreateSql([FromBody] KhachHangVM khachHang)
    {
        // kiểm tra model hợp lệ hay không
        // viết sql để thưc thi
        // sql injection 
        string sql = $"INSERT INTO KHACHHANG (Ten, Email,Sdt) VALUES ('{khachHang.Ten}','{khachHang.Email}','{khachHang.Sdt}')";

        // phuong nga'; Drop table nhanvien;
        // INSERT INTO KHACHHANG (Ten, Email,Sdt) VALUES ('phuong nga'); Drop table nhanvien;','{khachHang.Email}','{khachHang.Sdt}')
        var res = await _context.Database.ExecuteSqlRawAsync(sql);
        return new ResponseEntity(201, res, "Thêm thành công");
    }

    //
    [HttpPost("sql2")]
    // ExecuteSqlInterpolatedAsync gọn và an toàn hơn
    // an toàn hơn cách 1 
    public async Task<IActionResult> CreateSql2([FromBody] KhachHangVM khachHang)
    {
        // kiểm tra model hợp lệ hay không
        // viết sql để thưc thi
        // sql injection 
        // phuong nga'; Drop table nhanvien;
        // INSERT INTO KHACHHANG (Ten, Email,Sdt) VALUES ('phuong nga'); Drop table nhanvien;','{khachHang.Email}','{khachHang.Sdt}')
        FormattableString sql = $"INSERT INTO KHACHHANG (Ten, Email,Sdt) VALUES ({khachHang.Ten},{khachHang.Email},{khachHang.Sdt})";
        var res = await _context.Database.ExecuteSqlInterpolatedAsync(sql);
        await _context.SaveChangesAsync();
        return new ResponseEntity(201, res, "Thêm thành công");
    }


    // SELECT * FROM KHACHHANG WHERE TEN LIKE  N'nga'
    // api tìm kiếm khách hàng theo tên
    [HttpGet("search")] // api/khachhang/search?ten=""
    public async Task<IActionResult> SearchByName([FromQuery] string ten)
    {
        // dùng EF
        // .Where(k => )
        var res = await _context.KhachHangs.Where(k => k.Ten.Contains(ten)).ToListAsync();
        // tìm không []
        // tìm không thấy trả 404 => không thấy 
        return new ResponseEntity(200, res, "Tìm kiếm khách hàng thành công");
    }

    // dùng SQL RAW
    // demo lỗi sql injection
    [HttpGet("searchsql_bad")] // api/khachhang/searchsql_bad?ten=""
    public async Task<IActionResult> SearchByNameSqlBad([FromQuery] string ten)
    {
        // dùng EF
        // .Where(k => )
        // tìm không []
        // tìm không thấy trả 404 => không thấy 
        // %' OR 1=1 --
        string sql = $"SELECT * FROM KHACHHANG WHERE TEN LIKE N'%{ten}%'";

        var res = await _context.KhachHangs.FromSqlRaw(sql).ToListAsync();

        return new ResponseEntity(200, res, "Tìm kiếm khách hàng thành công");
    }

    // nếu không dùng được = ef và linq thì sẽ sql  -> Store proceduce database : chương trình mini viêt trên db , 
    
}




// ÔN BÀI

// EF ( Entity Framework) 6, EF Core
// Dapper ()
// hibernate (java dùng hơn)
// ORM -> Object Relational Mapping
// làm việc với database nhưng dung câu lệnh sql 
