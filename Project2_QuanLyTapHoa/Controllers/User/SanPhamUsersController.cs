using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2_QuanLyTapHoa.Models;

namespace Project2_QuanLyTapHoa.Controllers.User
{
    public class UserSanPhamsController : Controller
    {
        private readonly QuanLyTapHoaContext _context;

        public UserSanPhamsController(QuanLyTapHoaContext context)
        {
            _context = context;
        }

        // GET: /UserSanPhams/Index
        public async Task<IActionResult> Home()
        {
            var sanPhams = await _context.SanPhams
                .Include(s => s.MaLoaiNavigation)
                .Where(s => s.TrangThai == true) // chỉ lấy sản phẩm đang bán
                .ToListAsync();

            return View("~/Views/User/SanPhamUsers/Home.cshtml", sanPhams);
        }

        // GET: /UserSanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var sanPham = await _context.SanPhams
                .Include(s => s.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);

            if (sanPham == null) return NotFound();

            return View("~/Views/User/SanPhamUsers/Details.cshtml", sanPham);
        }
    }
}
