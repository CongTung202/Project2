using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NguyenCongTung_2310900051_Project2.Models;

public partial class CuahangTapHoaContext : DbContext
{
    public CuahangTapHoaContext()
    {
    }

    public CuahangTapHoaContext(DbContextOptions<CuahangTapHoaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

    public virtual DbSet<QuanTriVien> QuanTriViens { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BOKA-CHAN\\MSSQLSERVER01;Database=CUAHANG_TAP_HOA;uid=sa;pwd=12345678; MultipleActiveResultSets=True; TrustServerCertificate=True ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CHI_TIET__3214EC275A8C60F2");

            entity.ToTable("CHI_TIET_HOA_DON");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DonGiaMua)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("DON_GIA_MUA");
            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MA_HOA_DON");
            entity.Property(e => e.MaSanPham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MA_SAN_PHAM");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_CAP_NHAT");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_TAO");
            entity.Property(e => e.SoLuongMua).HasColumnName("SO_LUONG_MUA");
            entity.Property(e => e.ThanhTien)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("THANH_TIEN");
            entity.Property(e => e.TrangThai)
                .HasDefaultValue(true)
                .HasColumnName("TRANG_THAI");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasPrincipalKey(p => p.MaHoaDon)
                .HasForeignKey(d => d.MaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHI_TIET___MA_HO__5812160E");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasPrincipalKey(p => p.MaSanPham)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHI_TIET___MA_SA__59063A47");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HOA_DON__3214EC27DC5B0CEC");

            entity.ToTable("HOA_DON");

            entity.HasIndex(e => e.MaHoaDon, "UQ__HOA_DON__EFEAFCDA7080FF55").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("DIA_CHI");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("DIEN_THOAI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.HoTenKhachHang)
                .HasMaxLength(255)
                .HasColumnName("HO_TEN_KHACH_HANG");
            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MA_HOA_DON");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MA_KHACH_HANG");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_CAP_NHAT");
            entity.Property(e => e.NgayHoaDon)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("NGAY_HOA_DON");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_TAO");
            entity.Property(e => e.TongTriGia)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("TONG_TRI_GIA");
            entity.Property(e => e.TrangThai)
                .HasDefaultValue(true)
                .HasColumnName("TRANG_THAI");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HoaDons)
                .HasPrincipalKey(p => p.MaKhachHang)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOA_DON__MA_KHAC__52593CB8");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KHACH_HA__3214EC273BC3A3CA");

            entity.ToTable("KHACH_HANG");

            entity.HasIndex(e => e.Email, "UQ__KHACH_HA__161CF7248350A2A4").IsUnique();

            entity.HasIndex(e => e.MaKhachHang, "UQ__KHACH_HA__C5A28F79883868F4").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("DIA_CHI");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("DIEN_THOAI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.HoTen)
                .HasMaxLength(255)
                .HasColumnName("HO_TEN");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MA_KHACH_HANG");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MAT_KHAU");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_CAP_NHAT");
            entity.Property(e => e.NgayDangKy)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("NGAY_DANG_KY");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_TAO");
            entity.Property(e => e.TrangThai)
                .HasDefaultValue(true)
                .HasColumnName("TRANG_THAI");
        });

        modelBuilder.Entity<LoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LOAI_SAN__3214EC271B9302B1");

            entity.ToTable("LOAI_SAN_PHAM");

            entity.HasIndex(e => e.MaLoai, "UQ__LOAI_SAN__6D8E341D43A0F5F4").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaLoai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MA_LOAI");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_CAP_NHAT");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_TAO");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(255)
                .HasColumnName("TEN_LOAI");
            entity.Property(e => e.TrangThai)
                .HasDefaultValue(true)
                .HasColumnName("TRANG_THAI");
        });

        modelBuilder.Entity<QuanTriVien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QUAN_TRI__3214EC27EB2D6265");

            entity.ToTable("QUAN_TRI_VIEN");

            entity.HasIndex(e => e.TaiKhoan, "UQ__QUAN_TRI__275EFC14DA36EEB7").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MAT_KHAU");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_CAP_NHAT");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_TAO");
            entity.Property(e => e.TaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TAI_KHOAN");
            entity.Property(e => e.TrangThai)
                .HasDefaultValue(true)
                .HasColumnName("TRANG_THAI");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SAN_PHAM__3214EC2742D65CEB");

            entity.ToTable("SAN_PHAM");

            entity.HasIndex(e => e.MaSanPham, "UQ__SAN_PHAM__AEAADD68B91A035E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DonGia)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("DON_GIA");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .HasColumnName("HINH_ANH");
            entity.Property(e => e.MaLoai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MA_LOAI");
            entity.Property(e => e.MaSanPham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MA_SAN_PHAM");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_CAP_NHAT");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY_TAO");
            entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");
            entity.Property(e => e.TenSanPham)
                .HasMaxLength(255)
                .HasColumnName("TEN_SAN_PHAM");
            entity.Property(e => e.TrangThai)
                .HasDefaultValue(true)
                .HasColumnName("TRANG_THAI");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.SanPhams)
                .HasPrincipalKey(p => p.MaLoai)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SAN_PHAM__MA_LOA__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
