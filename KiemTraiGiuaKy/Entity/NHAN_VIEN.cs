namespace KiemTraiGiuaKy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NHAN_VIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHAN_VIEN()
        {
            DON_DAT_HANG = new HashSet<DON_DAT_HANG>();
            HOA_DON = new HashSet<HOA_DON>();
        }

        [Key]
        [StringLength(4)]
        public string MANHANVIEN { get; set; }

        [StringLength(30)]
        public string TENNHANVIEN { get; set; }

        [StringLength(50)]
        public string DIACHI { get; set; }

        [StringLength(10)]
        public string SDT { get; set; }

        [StringLength(5)]
        public string GIOITINH { get; set; }

        public DateTime? NGAYSINH { get; set; }

        [StringLength(12)]
        public string CMND_CCCD { get; set; }

        [StringLength(10)]
        public string TENDANGNHAP { get; set; }

        [StringLength(16)]
        public string MATKHAU { get; set; }

        public bool? VOHIEUHOA { get; set; }

        public bool? LAQUANLY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DON_DAT_HANG> DON_DAT_HANG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOA_DON> HOA_DON { get; set; }
    }
}
