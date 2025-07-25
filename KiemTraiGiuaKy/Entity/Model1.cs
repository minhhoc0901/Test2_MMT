using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KiemTraiGiuaKy.Entity
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<DON_DAT_HANG> DON_DAT_HANG { get; set; }
        public virtual DbSet<DONDATHANG_MATHANG> DONDATHANG_MATHANG { get; set; }
        public virtual DbSet<HOA_DON> HOA_DON { get; set; }
        public virtual DbSet<HOADON_MATHANG> HOADON_MATHANG { get; set; }
        public virtual DbSet<KHACH_HANG> KHACH_HANG { get; set; }
        public virtual DbSet<LOAI_MAT_HANG> LOAI_MAT_HANG { get; set; }
        public virtual DbSet<MAT_HANG> MAT_HANG { get; set; }
        public virtual DbSet<NHA_CUNG_CAP> NHA_CUNG_CAP { get; set; }
        public virtual DbSet<NHAN_VIEN> NHAN_VIEN { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DON_DAT_HANG>()
                .Property(e => e.MANCC)
                .IsUnicode(false);

            modelBuilder.Entity<DON_DAT_HANG>()
                .Property(e => e.MANV)
                .IsUnicode(false);

            modelBuilder.Entity<DON_DAT_HANG>()
                .HasMany(e => e.DONDATHANG_MATHANG)
                .WithRequired(e => e.DON_DAT_HANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DONDATHANG_MATHANG>()
                .Property(e => e.MAMH)
                .IsUnicode(false);

            modelBuilder.Entity<HOA_DON>()
                .Property(e => e.MANHANVIEN)
                .IsUnicode(false);

            modelBuilder.Entity<HOA_DON>()
                .HasMany(e => e.HOADON_MATHANG)
                .WithRequired(e => e.HOA_DON)
                .HasForeignKey(e => e.MAHD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOADON_MATHANG>()
                .Property(e => e.MAMH)
                .IsUnicode(false);

            modelBuilder.Entity<KHACH_HANG>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<LOAI_MAT_HANG>()
                .Property(e => e.MALOAI)
                .IsUnicode(false);

            modelBuilder.Entity<MAT_HANG>()
                .Property(e => e.MAMH)
                .IsUnicode(false);

            modelBuilder.Entity<MAT_HANG>()
                .Property(e => e.MALOAI)
                .IsUnicode(false);

            modelBuilder.Entity<MAT_HANG>()
                .HasMany(e => e.DONDATHANG_MATHANG)
                .WithRequired(e => e.MAT_HANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MAT_HANG>()
                .HasMany(e => e.HOADON_MATHANG)
                .WithRequired(e => e.MAT_HANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHA_CUNG_CAP>()
                .Property(e => e.MANCC)
                .IsUnicode(false);

            modelBuilder.Entity<NHA_CUNG_CAP>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NHA_CUNG_CAP>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<NHAN_VIEN>()
                .Property(e => e.MANHANVIEN)
                .IsUnicode(false);

            modelBuilder.Entity<NHAN_VIEN>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NHAN_VIEN>()
                .Property(e => e.CMND_CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<NHAN_VIEN>()
                .Property(e => e.TENDANGNHAP)
                .IsUnicode(false);

            modelBuilder.Entity<NHAN_VIEN>()
                .Property(e => e.MATKHAU)
                .IsUnicode(false);

            modelBuilder.Entity<NHAN_VIEN>()
                .HasMany(e => e.DON_DAT_HANG)
                .WithOptional(e => e.NHAN_VIEN)
                .HasForeignKey(e => e.MANV);
        }
    }
}
