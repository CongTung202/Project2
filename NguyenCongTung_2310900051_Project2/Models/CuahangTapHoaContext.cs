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
            entity.HasKey(e => e.Id).HasName("PK__CHI_TIET__3214EC2784155E97");

            entity.ToTable("CHI_TIET_HOA_DON");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DonGia)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("DON_GIA");
            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MA_HOA_DON");
            entity.Property(e => e.MaSanPham)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MA_SAN_PHAM");
            entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");
            entity.Property(e => e.ThanhTien)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("THANH_TIEN");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasPrincipalKey(p => p.MaHoaDon)
                .HasForeignKey(d => d.MaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHI_TIET___MA_HO__48CFD27E");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasPrincipalKey(p => p.MaSanPham)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHI_TIET___MA_SA__49C3F6B7");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HOA_DON__3214EC272C55E2EC");

            entity.ToTable("HOA_DON");

            entity.HasIndex(e => e.MaHoaDon, "UQ__HOA_DON__EFEAFCDA78556580").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MA_HOA_DON");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MA_KHACH_HANG");
            entity.Property(e => e.NgayHoaDon)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("NGAY_HOA_DON");
            entity.Property(e => e.TongTriGia)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("TONG_TRI_GIA");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HoaDons)
                .HasPrincipalKey(p => p.MaKhachHang)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOA_DON__MA_KHAC__45F365D3");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KHACH_HA__3214EC27A5A9FBD4");

            entity.ToTable("KHACH_HANG");

            entity.HasIndex(e => e.Email, "UQ__KHACH_HA__161CF7249B590F71").IsUnique();

            entity.HasIndex(e => e.MaKhachHang, "UQ__KHACH_HA__C5A28F7913C1FD8E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("DIA_CHI");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("DIEN_THOAI");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("HO_TEN");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MA_KHACH_HANG");
        });

        modelBuilder.Entity<LoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LOAI_SAN__3214EC27D26A1008");

            entity.ToTable("LOAI_SAN_PHAM");

            entity.HasIndex(e => e.MaLoai, "UQ__LOAI_SAN__6D8E341D3E03DE0F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaLoai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MA_LOAI");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(100)
                .HasColumnName("TEN_LOAI");
        });

        modelBuilder.Entity<QuanTriVien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QUAN_TRI__3214EC27F13A8FF3");

            entity.ToTable("QUAN_TRI_VIEN");

            entity.HasIndex(e => e.TaiKhoan, "UQ__QUAN_TRI__275EFC14536E9487").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MAT_KHAU");
            entity.Property(e => e.TaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TAI_KHOAN");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SAN_PHAM__3214EC277326213E");

            entity.ToTable("SAN_PHAM");

            entity.HasIndex(e => e.MaSanPham, "UQ__SAN_PHAM__AEAADD68C71347FB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DonGia)
                .HasColumnType("decimal(15, 2)")
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
            entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");
            entity.Property(e => e.TenSanPham)
                .HasMaxLength(255)
                .HasColumnName("TEN_SAN_PHAM");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.SanPhams)
                .HasPrincipalKey(p => p.MaLoai)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SAN_PHAM__MA_LOA__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
