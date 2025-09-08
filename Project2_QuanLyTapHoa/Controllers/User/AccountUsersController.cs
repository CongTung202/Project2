using Microsoft.AspNetCore.Mvc;
using Project2_QuanLyTapHoa.Models;

namespace Project2_QuanLyTapHoa.Controllers.User
{
    public class AccountUsersController : Controller
    {
        private readonly QuanLyTapHoaContext _context;

        public AccountUsersController(QuanLyTapHoaContext context)
        {
            _context = context;
        }

        // GET: User/AccountUsers/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/User/AccountUsers/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(string email, string matKhau)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

            var kh = _context.KhachHangs
                .FirstOrDefault(k => k.Email == email && k.MatKhau == matKhau && k.TrangThai == true);

            if (kh != null)
            {
                // Lưu session
                HttpContext.Session.SetInt32("MaKh", kh.MaKh);
                HttpContext.Session.SetString("HoTen", kh.HoTen);
                HttpContext.Session.SetString("UserEmail", kh.Email ?? "");

                // ✅ Redirect về giao diện User
                return RedirectToAction("Home", "UserSanPhams");
            }

            ViewBag.Error = "Email hoặc mật khẩu không đúng!";
            return View();
        }

        // GET: User/AccountUsers/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // xoá toàn bộ session
            return RedirectToAction("Index", "Home");
        }
    }
}
