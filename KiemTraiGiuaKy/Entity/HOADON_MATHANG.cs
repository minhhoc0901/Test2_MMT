namespace KiemTraiGiuaKy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HOADON_MATHANG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MAHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string MAMH { get; set; }

        public int? SOLUONGMUA { get; set; }

        public double? THANHTIEN { get; set; }

        public virtual HOA_DON HOA_DON { get; set; }

        public virtual MAT_HANG MAT_HANG { get; set; }
    }
}
