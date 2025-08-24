using System;
using System.Collections.Generic;

namespace NguyenCongTung_2310900051_Project2.Models;

public partial class QuanTriVien
{
    public int Id { get; set; }

    public string TaiKhoan { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public bool TrangThai { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }
}
