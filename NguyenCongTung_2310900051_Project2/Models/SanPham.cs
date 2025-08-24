using System;
using System.Collections.Generic;

namespace NguyenCongTung_2310900051_Project2.Models;

public partial class SanPham
{
    public int Id { get; set; }

    public string MaSanPham { get; set; } = null!;

    public string TenSanPham { get; set; } = null!;

    public decimal DonGia { get; set; }

    public int SoLuong { get; set; }

    public string? HinhAnh { get; set; }

    public string MaLoai { get; set; } = null!;

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual LoaiSanPham MaLoaiNavigation { get; set; } = null!;
}
