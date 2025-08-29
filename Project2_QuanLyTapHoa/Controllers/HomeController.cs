using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2_QuanLyTapHoa.Filters;
using Project2_QuanLyTapHoa.Models;
using System.Diagnostics;

namespace Project2_QuanLyTapHoa.Controllers
{
    [AuthorizeAdmin]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanLyTapHoaContext _context; // thêm dòng này

        // inject DbContext
        public HomeController(ILogger<HomeController> logger, QuanLyTapHoaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalSanPham = await _context.SanPhams.CountAsync();
            ViewBag.TotalKhachHang = await _context.KhachHangs.CountAsync();
            ViewBag.TotalHoaDon = await _context.HoaDons.CountAsync();
            ViewBag.TotalQTV = await _context.QuanTriViens.CountAsync();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
