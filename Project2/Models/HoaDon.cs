using System;
using System.Collections.Generic;

namespace Project2.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public DateTime NgayLap { get; set; }

    public decimal TongTien { get; set; }

    public int? MaKh { get; set; }

    public int MaSp { get; set; }

    public bool TrangThai { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
