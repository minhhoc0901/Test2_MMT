namespace KiemTraiGiuaKy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MAT_HANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MAT_HANG()
        {
            DONDATHANG_MATHANG = new HashSet<DONDATHANG_MATHANG>();
            HOADON_MATHANG = new HashSet<HOADON_MATHANG>();
        }

        [Key]
        [StringLength(5)]
        public string MAMH { get; set; }

        [StringLength(50)]
        public string TENMH { get; set; }

        public long? GIABAN { get; set; }

        [StringLength(10)]
        public string DVT { get; set; }

        [StringLength(5)]
        public string MALOAI { get; set; }

        [StringLength(1000)]
        public string MOTA { get; set; }

        public bool? VOHIEUHOA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONDATHANG_MATHANG> DONDATHANG_MATHANG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON_MATHANG> HOADON_MATHANG { get; set; }

        public virtual LOAI_MAT_HANG LOAI_MAT_HANG { get; set; }
    }
}
