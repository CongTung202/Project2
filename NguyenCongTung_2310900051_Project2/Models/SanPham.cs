using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenCongTung_2310900051_Project2.Models;

public partial class SanPham
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
    [StringLength(10, ErrorMessage = "Mã sản phẩm tối đa 10 ký tự")]
    public string MaSanPham { get; set; } = null!;

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(255, ErrorMessage = "Tên sản phẩm tối đa 255 ký tự")]
    public string TenSanPham { get; set; } = null!;

    public string? HinhAnh { get; set; }

    [Required(ErrorMessage = "Số lượng không được để trống")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là số không âm")]
    public int SoLuong { get; set; }

    [Required(ErrorMessage = "Đơn giá không được để trống")]
    [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải là số không âm")]
    public decimal DonGia { get; set; }

    [Required(ErrorMessage = "Mã loại không được để trống")]
    [StringLength(10, ErrorMessage = "Mã loại tối đa 10 ký tự")]
    public string MaLoai { get; set; } = null!;

    public bool TrangThai { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual LoaiSanPham MaLoaiNavigation { get; set; } = null!;
}
