using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2.Models;

namespace Project2.Controllers
{
    public class HoaDonsController : Controller
    {
        private readonly QuanLyBanHangContext _context;

        public HoaDonsController(QuanLyBanHangContext context)
        {
            _context = context;
        }

        // GET: HoaDons
        public async Task<IActionResult> Index()
        {
            var list = await _context.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaSpNavigation)
                .ToListAsync();
            return View(list);
        }

        // Partial load danh sách hóa đơn
        public async Task<IActionResult> LoadHoaDonList()
        {
            var list = await _context.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaSpNavigation)
                .ToListAsync();
            return PartialView("_HoaDonList", list);
        }

        // GET: HoaDons/Create
        public IActionResult Create()
        {
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh");
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp");
            return View();
        }

        // POST: HoaDons/Create Ajax
        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] HoaDonCreateDto hoaDonDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var hoaDon = new HoaDon
            {
                NgayLap = DateTime.Now,
                TongTien = hoaDonDto.TongTien,
                MaKh = hoaDonDto.MaKh,
                MaSp = hoaDonDto.MaSp,
                TrangThai = true
            };

            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // GET: HoaDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null) return NotFound();

            var dto = new HoaDonEditDto
            {
                MaHd = hoaDon.MaHd,
                TongTien = hoaDon.TongTien,
                MaKh = hoaDon.MaKh,
                MaSp = hoaDon.MaSp,
                TrangThai = hoaDon.TrangThai
            };

            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", dto.MaKh);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", dto.MaSp);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HoaDonEditDto dto)
        {
            if (id != dto.MaHd)
                return Json(new { success = false, message = "Không tìm thấy hóa đơn" });

            if (ModelState.IsValid)
            {
                var hoaDon = await _context.HoaDons.FindAsync(id);
                if (hoaDon == null)
                    return Json(new { success = false, message = "Hóa đơn không tồn tại" });

                // Update các field
                hoaDon.TongTien = dto.TongTien;
                hoaDon.MaKh = dto.MaKh;
                hoaDon.MaSp = dto.MaSp;
                hoaDon.TrangThai = dto.TrangThai;
                hoaDon.NgayLap = DateTime.Now; // cập nhật ngày hiện tại

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cập nhật hóa đơn thành công" });
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
        }


        // GET: HoaDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHd == id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }


        // GET: HoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHd == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon != null)
            {
                _context.HoaDons.Remove(hoaDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.MaHd == id);
        }
    }
}
