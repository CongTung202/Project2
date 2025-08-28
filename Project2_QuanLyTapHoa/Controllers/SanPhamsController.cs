﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2_QuanLyTapHoa.Models;

namespace Project2_QuanLyTapHoa.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly QuanLyTapHoaContext _context;

        public SanPhamsController(QuanLyTapHoaContext context)
        {
            _context = context;
        }

        // GET: SanPhams
        public async Task<IActionResult> Index()
        {
            var quanLyTapHoaContext = _context.SanPhams.Include(s => s.MaLoaiNavigation);
            return View(await quanLyTapHoaContext.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.LoaiSanPhams, "MaLoai", "MaLoai");
            return View();
        }

        // POST: SanPhams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,MoTa,DonGia,SoLuong,HinhAnh,MaLoai,PhanTramGiam,TrangThai")] SanPham sanPham, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    // đường dẫn thư mục wwwroot/images/sanpham
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "sanpham");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // tạo tên file duy nhất
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(folderPath, fileName);

                    // lưu file vật lý
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // lưu đường dẫn tương đối vào DB
                    sanPham.HinhAnh = "/images/sanpham/" + fileName;
                }

                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MaLoai"] = new SelectList(_context.LoaiSanPhams, "MaLoai", "MaLoai", sanPham.MaLoai);
            return View(sanPham);
        }
        // GET: SanPhams/Edit/5
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

            // Map sang DTO
            var dto = new SanPhamDto
            {
                MaSp = sanPham.MaSp,
                TenSp = sanPham.TenSp,
                MoTa = sanPham.MoTa,
                DonGia = sanPham.DonGia,
                SoLuong = sanPham.SoLuong,
                HinhAnh = sanPham.HinhAnh,
                MaLoai = sanPham.MaLoai,
                PhanTramGiam = sanPham.PhanTramGiam,
                TrangThai = sanPham.TrangThai
            };

            ViewData["MaLoai"] = new SelectList(_context.LoaiSanPhams, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditAjax([FromForm] SanPhamDto dto, IFormFile? file)
        {
            var sanPham = await _context.SanPhams.FirstOrDefaultAsync(x => x.MaSp == dto.MaSp);
            if (sanPham == null) return Json(new { success = false, message = "Không tìm thấy sản phẩm" });

            // cập nhật dữ liệu text
            sanPham.TenSp = dto.TenSp;
            sanPham.MoTa = dto.MoTa;
            sanPham.DonGia = dto.DonGia;
            sanPham.SoLuong = dto.SoLuong;
            sanPham.MaLoai = dto.MaLoai;
            sanPham.PhanTramGiam = dto.PhanTramGiam;
            sanPham.TrangThai = dto.TrangThai;

            // xử lý ảnh
            if (file != null && file.Length > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "sanpham");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                sanPham.HinhAnh = "/images/sanpham/" + fileName;
            }
            // nếu không có file mới -> giữ nguyên ảnh cũ (không cần gán gì thêm)

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Cập nhật thành công" });
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
                .FirstOrDefaultAsync(m => m.MaSp == id);
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
            return _context.SanPhams.Any(e => e.MaSp == id);
        }
    }
}
