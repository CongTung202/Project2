using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2_QuanLyTapHoa.Models;

namespace Project2_QuanLyTapHoa.Controllers
{
    public class DanhGiaSanPhamsController : Controller
    {
        private readonly QuanLyTapHoaContext _context;

        public DanhGiaSanPhamsController(QuanLyTapHoaContext context)
        {
            _context = context;
        }

        // GET: DanhGiaSanPhams
        public async Task<IActionResult> Index()
        {
            var quanLyTapHoaContext = _context.DanhGiaSanPhams.Include(d => d.MaKhNavigation).Include(d => d.MaSpNavigation);
            return View(await quanLyTapHoaContext.ToListAsync());
        }

        // GET: DanhGiaSanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGiaSanPham = await _context.DanhGiaSanPhams
                .Include(d => d.MaKhNavigation)
                .Include(d => d.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaDanhGia == id);
            if (danhGiaSanPham == null)
            {
                return NotFound();
            }

            return View(danhGiaSanPham);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh");
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DanhGiaSanPhamDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new DanhGiaSanPham
            {
                MaKh = dto.MaKh,
                MaSp = dto.MaSp,
                SoSao = dto.SoSao,
                NoiDung = dto.NoiDung,
                LaYeuThich = dto.LaYeuThich,
                NgayTao = DateTime.Now,
                TrangThai = true
            };

            _context.DanhGiaSanPhams.Add(entity);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Thêm đánh giá thành công" });
        }
        // GET: DanhGiaSanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.DanhGiaSanPhams.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = new DanhGiaSanPhamDto
            {
                MaDanhGia = entity.MaDanhGia,
                MaKh = entity.MaKh,
                MaSp = entity.MaSp,
                SoSao = entity.SoSao,
                NoiDung = entity.NoiDung,
                LaYeuThich = entity.LaYeuThich,
                NgayTao = entity.NgayTao,
                TrangThai = entity.TrangThai
            };

            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", dto.MaKh);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", dto.MaSp);

            return View(dto);
        }

        // POST: DanhGiaSanPhams/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] DanhGiaSanPhamDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _context.DanhGiaSanPhams.FindAsync(dto.MaDanhGia);
            if (entity == null)
            {
                return NotFound();
            }

            entity.MaKh = dto.MaKh;
            entity.MaSp = dto.MaSp;
            entity.SoSao = dto.SoSao;
            entity.NoiDung = dto.NoiDung;
            entity.LaYeuThich = dto.LaYeuThich;
            entity.NgayTao = dto.NgayTao;
            entity.TrangThai = dto.TrangThai;

            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: DanhGiaSanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGiaSanPham = await _context.DanhGiaSanPhams
                .Include(d => d.MaKhNavigation)
                .Include(d => d.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaDanhGia == id);
            if (danhGiaSanPham == null)
            {
                return NotFound();
            }

            return View(danhGiaSanPham);
        }

        // POST: DanhGiaSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhGiaSanPham = await _context.DanhGiaSanPhams.FindAsync(id);
            if (danhGiaSanPham != null)
            {
                _context.DanhGiaSanPhams.Remove(danhGiaSanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhGiaSanPhamExists(int id)
        {
            return _context.DanhGiaSanPhams.Any(e => e.MaDanhGia == id);
        }
    }
}
