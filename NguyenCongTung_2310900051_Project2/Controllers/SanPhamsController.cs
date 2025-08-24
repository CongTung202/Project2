using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenCongTung_2310900051_Project2.Models;

namespace NguyenCongTung_2310900051_Project2.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly CuahangTapHoaContext _context;

        public SanPhamsController(CuahangTapHoaContext context)
        {
            _context = context;
        }

        // GET: SanPhams
        public async Task<IActionResult> Index()
        {
            var cuahangTapHoaContext = _context.SanPhams.Include(s => s.MaLoaiNavigation);
            return View(await cuahangTapHoaContext.ToListAsync());
        }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.LoaiSanPhams, "MaLoai", "TenLoai");
            return View();
        }

        // POST: SanPhams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaSanPham,TenSanPham,HinhAnh,SoLuong,DonGia,MaLoai,TrangThai")] SanPham sanPham, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Upload hình ảnh
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(ImageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sanpham", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        sanPham.HinhAnh = "/images/sanpham" + fileName;
                    }

                    sanPham.NgayTao = DateTime.Now;
                    sanPham.NgayCapNhat = DateTime.Now;

                    _context.Add(sanPham);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // lỗi trùng mã sản phẩm
                    if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                    {
                        ModelState.AddModelError("MaSanPham", "Mã sản phẩm đã tồn tại.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không thể lưu sản phẩm. Vui lòng thử lại.");
                    }
                }
            }

            ViewData["MaLoai"] = new SelectList(_context.LoaiSanPhams, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiSanPhams, "MaLoai", "MaLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaSanPham,TenSanPham,HinhAnh,SoLuong,DonGia,MaLoai,TrangThai,NgayTao,NgayCapNhat")] SanPham sanPham)
        {
            if (id != sanPham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiSanPhams, "MaLoai", "MaLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.Id == id);
        }
    }
}
