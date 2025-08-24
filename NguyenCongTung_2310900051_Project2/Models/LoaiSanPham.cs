using System.ComponentModel.DataAnnotations;

namespace NguyenCongTung_2310900051_Project2.Models
{
    public partial class LoaiSanPham
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã loại bắt buộc nhập")]
        [StringLength(10, ErrorMessage = "Mã loại tối đa 10 ký tự")]
        [Display(Name = "Mã loại")]
        public string MaLoai { get; set; } = null!;

        [Required(ErrorMessage = "Tên loại bắt buộc nhập")]
        [StringLength(255, ErrorMessage = "Tên loại tối đa 255 ký tự")]
        [Display(Name = "Tên loại")]
        public string TenLoai { get; set; } = null!;

        public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
    }
}
