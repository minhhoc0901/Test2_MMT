namespace KiemTraiGiuaKy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NHA_CUNG_CAP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHA_CUNG_CAP()
        {
            DON_DAT_HANG = new HashSet<DON_DAT_HANG>();
        }

        [Key]
        [StringLength(5)]
        public string MANCC { get; set; }

        [StringLength(50)]
        public string TENNCC { get; set; }

        [StringLength(50)]
        public string DIACHI { get; set; }

        [StringLength(10)]
        public string SDT { get; set; }

        [StringLength(30)]
        public string EMAIL { get; set; }

        public bool? VOHIEUHOA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DON_DAT_HANG> DON_DAT_HANG { get; set; }
    }
}
