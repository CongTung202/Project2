using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenCongTung_2310900051_Project2.Models;

public partial class QuanTriVien
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Tài khoản bắt buộc nhập")]
    [StringLength(20, ErrorMessage = "Tài khoản tối đa 20 ký tự")]
    public string TaiKhoan { get; set; } = null!;
    [Required(ErrorMessage = "Mật khẩu bắt buộc nhập")]
    [StringLength(20, ErrorMessage = "Mật khẩu tối đa 20 ký tự")]
    public string MatKhau { get; set; } = null!;
}
