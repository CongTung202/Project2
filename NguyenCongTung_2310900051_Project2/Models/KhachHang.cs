using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // thêm namespace này

namespace NguyenCongTung_2310900051_Project2.Models
{
    public partial class KhachHang
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        [StringLength(20, ErrorMessage = "Mã khách hàng tối đa 20 ký tự")]
        public string MaKhachHang { get; set; } = null!;

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên tối đa 100 ký tự")]
        public string HoTen { get; set; } = null!;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15, ErrorMessage = "Số điện thoại tối đa 15 ký tự")]
        public string? DienThoai { get; set; }

        [StringLength(200, ErrorMessage = "Địa chỉ tối đa 200 ký tự")]
        public string? DiaChi { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}
