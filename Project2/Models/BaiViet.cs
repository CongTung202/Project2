using System;
using System.Collections.Generic;

namespace Project2.Models;

public partial class BaiViet
{
    public int MaBv { get; set; }

    public string TieuDe { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public DateTime NgayDang { get; set; }

    public string? HinhAnh { get; set; }

    public int? MaQtv { get; set; }

    public bool TrangThai { get; set; }

    public virtual QuanTriVien? MaQtvNavigation { get; set; }
}
