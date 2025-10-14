//api_controller
using Microsoft.AspNetCore.Mvc;
using webapi.ModelsDemo;

namespace webapi.Controllers;


// Router định tuyến đường dẫn
// api là phần cố định của url api => localhost:5001/api/
// [controller] : thay thế = tên file bỏ chữ Controller
// SanPhamController => 109.193.2.4/api/sanpham
[Route("/api/[controller]")]
// kích hoạt tính năng của controller, tính năng đặc biettj  của webapi
// tự trả về kiểu 404 khi không tìm thấy
// tự động suy luận nguồn dữ liệu
// form data (dự liệu file , hình ảnh ...)
// 
[ApiController]
public class SanPhamController : ControllerBase
{
    public static List<SanPham> _lst = new List<SanPham>()
    {
        new SanPham(){ Id=1, Ten="Iphone 14", Gia=2000, MaPhanLoai=1},
        new SanPham(){ Id=2, Ten="Samsung S23", Gia=1800, MaPhanLoai=1},
        new SanPham(){ Id=3, Ten="Xiaomi 13", Gia=1500, MaPhanLoai=1},
        new SanPham(){ Id=4, Ten="Dell XPS 13", Gia=2500, MaPhanLoai=2},
        new SanPham(){ Id=5, Ten="Macbook Pro 14", Gia=3000, MaPhanLoai=2},
        new SanPham(){ Id=6, Ten="Asus Zenbook", Gia=2200, MaPhanLoai=2},
    };
    //  phương thức get lấy dữ liệu
    [HttpGet]
    //IActionResult: interface kiểu trả về của http trong webapi

    public async Task<IActionResult> Get()
    {
        //return Ok(new { id = 1, Ten = "Nga" });// trả về mã 200
        // return NotFound(); // trả về mã 404
        // return BadRequest(); // trả về mã 400   
        // return StatusCode(500); // trả về mã 500
        // 200, 201, 204 => thành công
        // 
        // trả về dự liệu của SanPham
        return Ok(_lst);
    }
    // lấy 1 san phẩm dựa vào id
    // api/sanpham/id
    [HttpGet("{id}")]
    // có thể bỏ [FromRoute] vì id trùng tên với tham số trong route, tự động suy luận
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        // tìm sp có id tương ứng trong lst
        SanPham sp = _lst.FirstOrDefault(p => p.Id == id);
        // nếu tìm được thì trả vê 200 (OK)
        if (sp != null)
            return Ok(sp);
        // nếu không tìm được trả về 404 (NotFound)
        return NotFound("Không tìm thấy sản phẩm với id = " + id);

    }
    // tìm sp theo tên 
    // api/sanpham/timkiem?ten=abc
    //có thể bỏ [FromQuery] , tự động suy luận
    [HttpGet("timkiem")]
    public async Task<IActionResult> TimKiem([FromQuery] string ten)
    {
        // tìm sp có tên tương ứng
        var res = _lst.Where(p => p.Ten.ToLower().Contains(ten.ToLower()));
        // trả về bằng chuẩn response
        return new ResponseEntity(200, res, "Tìm kiếm thành công");
    }

    // thêm mới sp 
    // REST : POST thêm mới => 201
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] SanPham sp)
    {
        // kiểm tra id trùng không
        var existing = _lst.FirstOrDefault(p => p.Id == sp.Id);
        // có trùng trả về 400 
        if (existing != null)
            return new ResponseEntity(400, null, "Id đã tồn tại");
        // không trùng thêm vào danh sách
        _lst.Add(sp);
        // trả về 201
        return new ResponseEntity(201, sp, "Thêm mới thành công");
    }
    // sửa sp
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SanPham sp)
    {
        // tìm sp có id tương ứng
        var find = _lst.FirstOrDefault(p => p.Id == id);
        // nếu không tìm thấy trả về 404
        if (find == null)
            return new ResponseEntity(400, null, "Không tìm thấy sản phẩm với id = " + id);

        // nếu tìm thấy cập nhật thông tin sp
        // vì List là tham chiếu nên sửa trực tiếp
        find.Ten = sp.Ten;
        find.Gia = sp.Gia;
        find.MaPhanLoai = sp.MaPhanLoai;
        // trả về 200
        return new ResponseEntity(200, find, "Cập nhật thành công");
        
    }

    // xoá sp

    [HttpDelete("{id}")]
    // đã bỏ [FromRoute], tự động suy luận
    public async Task<IActionResult> Delete(int id)
    {

        // tìm sp có id tương ứng
        var find = _lst.FirstOrDefault(p => p.Id == id);

        // nếu không tìm thấy trả về 404
        if (find == null)
            return new ResponseEntity(404, null, "Không tìm thấy sản phẩm với id = " + id);

        // nếu tìm thấy xoá sp khỏi danh sách   
        _lst.Remove(find);
        // trả về 200
        return new ResponseEntity(200, find, "Xoá sản phẩm thành công");
    }



}



