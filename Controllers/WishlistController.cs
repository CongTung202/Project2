using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NemeShop.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace NemeShop.Controllers
{
    public class WishlistController : Controller
    {
        private readonly QuanLyTapHoaContext _context;

        public WishlistController(QuanLyTapHoaContext context)
        {
            _context = context;
        }

        // GET: /Wishlist/MyWishlist
        [HttpGet]
        public async Task<IActionResult> MyWishlist()
        {
            var maKh = HttpContext.Session.GetInt32("MaKh");
            if (maKh == null) return RedirectToAction("Login", "Account");

            // Dùng projection JOIN để trả về DTO tránh navigation khi serialize
            var list = await (
                from d in _context.DanhGiaSanPhams
                join s in _context.SanPhams on d.MaSp equals s.MaSp
                where d.MaKh == maKh && d.LaYeuThich
                orderby d.NgayTao descending
                select new DanhGiaSanPhamDto
                {
                    MaDanhGia = d.MaDanhGia,
                    MaKh = d.MaKh,
                    MaSp = d.MaSp,
                    SoSao = d.SoSao,
                    NoiDung = d.NoiDung,
                    LaYeuThich = d.LaYeuThich,
                    NgayTao = d.NgayTao,
                    TrangThai = d.TrangThai,
                    TenSanPham = s.TenSp
                }
            ).ToListAsync();

            return View("MyWishlist", list);
        }

        // POST: /Wishlist/AddToWishlist  (JSON body)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWishlist([FromBody] DanhGiaSanPhamDto dto)
        {
            var maKh = HttpContext.Session.GetInt32("MaKh");
            if (maKh == null) return Unauthorized(new { success = false, message = "Bạn cần đăng nhập để thêm vào yêu thích" });

            if (dto == null || dto.MaSp == null) return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ" });

            int maSp = dto.MaSp.Value;

            var exists = await _context.DanhGiaSanPhams
                .FirstOrDefaultAsync(x => x.MaKh == maKh && x.MaSp == maSp);

            if (exists != null)
            {
                if (exists.LaYeuThich)
                {
                    return Json(new { success = true, message = "Sản phẩm đã có trong danh sách yêu thích" });
                }
                exists.LaYeuThich = true;
                exists.NgayTao = DateTime.Now;
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Đã thêm lại vào danh sách yêu thích" });
            }

            var entity = new DanhGiaSanPham
            {
                MaKh = maKh.Value,
                MaSp = maSp,
                SoSao = 5,
                NoiDung = dto.NoiDung ?? "Đã thêm vào yêu thích",
                LaYeuThich = true,
                NgayTao = DateTime.Now,
                TrangThai = true
            };

            _context.DanhGiaSanPhams.Add(entity);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã thêm vào danh sách yêu thích" });
        }

        // POST: /Wishlist/RemoveFromWishlist (JSON body)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromWishlist([FromBody] DanhGiaSanPhamDto dto)
        {
            var maKh = HttpContext.Session.GetInt32("MaKh");
            if (maKh == null) return Unauthorized(new { success = false, message = "Bạn cần đăng nhập để xoá khỏi yêu thích" });

            if (dto == null || dto.MaSp == null) return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ" });

            int maSp = dto.MaSp.Value;

            var item = await _context.DanhGiaSanPhams
                .FirstOrDefaultAsync(x => x.MaKh == maKh && x.MaSp == maSp && x.LaYeuThich);

            if (item == null) return NotFound(new { success = false, message = "Không tìm thấy sản phẩm trong yêu thích" });

            // giữ record, chỉ đổi flag
            item.LaYeuThich = false;
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã xóa khỏi danh sách yêu thích" });
        }

        // GET: /Wishlist/GetWishlistJson (tùy chọn dùng Ajax load)
        [HttpGet]
        public async Task<IActionResult> GetWishlistJson()
        {
            var maKh = HttpContext.Session.GetInt32("MaKh");
            if (maKh == null) return Unauthorized();

            var list = await (
                from d in _context.DanhGiaSanPhams
                join s in _context.SanPhams on d.MaSp equals s.MaSp
                where d.MaKh == maKh && d.LaYeuThich
                orderby d.NgayTao descending
                select new DanhGiaSanPhamDto
                {
                    MaDanhGia = d.MaDanhGia,
                    MaKh = d.MaKh,
                    MaSp = d.MaSp,
                    SoSao = d.SoSao,
                    NoiDung = d.NoiDung,
                    LaYeuThich = d.LaYeuThich,
                    NgayTao = d.NgayTao,
                    TrangThai = d.TrangThai,
                    TenSanPham = s.TenSp
                }
            ).ToListAsync();

            return Json(list);
        }
    }
}
