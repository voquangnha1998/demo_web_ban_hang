namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Oder")]
    public partial class Oder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oder()
        {
            OderDetails = new HashSet<OderDetail>();
        }

        public int ID { get; set; }

        public int? UserID { get; set; }
        [Display(Name = "Tên khách nhận")]
        [StringLength(100)]
        public string ShipName { get; set; }
        [Display(Name = "SĐT Ship")]
        [StringLength(100)]
        public string ShipPhone { get; set; }
        [Display(Name = "Địa chỉ Ship")]
        [StringLength(100)]
        public string ShipAddress { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
       
        public virtual User User { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OderDetail> OderDetails { get; set; }
    }
}
