using System;
using System.Collections.Generic;

namespace NguyenCongTung_2310900051_Project2.Models;

public partial class HoaDon
{
    public int Id { get; set; }

    public string MaHoaDon { get; set; } = null!;

    public string MaKhachHang { get; set; } = null!;

    public DateOnly? NgayHoaDon { get; set; }

    public decimal TongTriGia { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;
}
