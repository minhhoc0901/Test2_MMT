namespace KiemTraiGiuaKy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DON_DAT_HANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DON_DAT_HANG()
        {
            DONDATHANG_MATHANG = new HashSet<DONDATHANG_MATHANG>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MADDH { get; set; }

        [StringLength(5)]
        public string MANCC { get; set; }

        public DateTime? NGAYDAT { get; set; }

        [StringLength(4)]
        public string MANV { get; set; }

        public virtual NHA_CUNG_CAP NHA_CUNG_CAP { get; set; }

        public virtual NHAN_VIEN NHAN_VIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONDATHANG_MATHANG> DONDATHANG_MATHANG { get; set; }
    }
}
