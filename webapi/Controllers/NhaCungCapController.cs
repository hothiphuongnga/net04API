namespace webapi.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using webapi.Data;
    using webapi.Mapping;
    using webapi.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapController : ControllerBase
    {

        private readonly QuanLyBanHangContext _context;
        private readonly INhaCungCapMapping _nhaCungCapMapping;

        public NhaCungCapController(
            QuanLyBanHangContext context,
            INhaCungCapMapping nhaCungCapMapping)
        {
            _context = context;
            _nhaCungCapMapping = nhaCungCapMapping;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            //
            NhaCungCap ncc = await _context.NhaCungCaps.FindAsync(id);

            // mình muón return về NCCVM
            var nccVM = _nhaCungCapMapping.ToVM(ncc);


            return Ok(nccVM);
        }
    }
}