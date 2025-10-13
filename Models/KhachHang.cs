using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NemeShop.Models
{
    [Table("KhachHang")]
    public partial class KhachHang
    {
        [Key]
        [Column("MaKH")] // QUAN TRỌNG: Map đúng tên cột
        public int MaKh { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; } = null!;

        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; } = null!;

        [StringLength(15)]
        public string? DienThoai { get; set; }

        [StringLength(200)]
        public string? DiaChi { get; set; }

        public bool TrangThai { get; set; }

        public virtual ICollection<DanhGiaSanPham> DanhGiaSanPhams { get; set; } = new List<DanhGiaSanPham>();

        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}