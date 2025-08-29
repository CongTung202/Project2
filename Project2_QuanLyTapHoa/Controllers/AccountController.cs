using Microsoft.AspNetCore.Mvc;
using Project2_QuanLyTapHoa.Models;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    private readonly QuanLyTapHoaContext _context;

    public AccountController(QuanLyTapHoaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string matKhau)
    {
        var user = await _context.QuanTriViens
            .FirstOrDefaultAsync(x => x.Email == email && x.MatKhau == matKhau);

        if (user != null)
        {
            // Lưu thông tin user vào session
            HttpContext.Session.SetString("UserEmail", user.Email);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Sai email hoặc mật khẩu";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
