using System;
using System.Collections.Generic;

namespace Project2.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public string TenSp { get; set; } = null!;

    public string? MoTa { get; set; }

    public decimal DonGia { get; set; }

    public int SoLuong { get; set; }

    public string? HinhAnh { get; set; }

    public int? MaLoai { get; set; }

    public bool TrangThai { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual LoaiSanPham? MaLoaiNavigation { get; set; }
}
