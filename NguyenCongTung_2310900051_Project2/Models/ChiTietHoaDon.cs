using System;
using System.Collections.Generic;

namespace NguyenCongTung_2310900051_Project2.Models;

public partial class ChiTietHoaDon
{
    public int Id { get; set; }

    public string MaHoaDon { get; set; } = null!;

    public string MaSanPham { get; set; } = null!;

    public int SoLuong { get; set; }

    public decimal DonGia { get; set; }

    public decimal ThanhTien { get; set; }

    public virtual HoaDon MaHoaDonNavigation { get; set; } = null!;

    public virtual SanPham MaSanPhamNavigation { get; set; } = null!;
}
