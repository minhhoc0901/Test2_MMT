namespace KiemTraiGiuaKy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DONDATHANG_MATHANG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MADDH { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string MAMH { get; set; }

        public int? SOLUONG { get; set; }

        public double? DONGIA { get; set; }

        public virtual DON_DAT_HANG DON_DAT_HANG { get; set; }

        public virtual MAT_HANG MAT_HANG { get; set; }
    }
}
