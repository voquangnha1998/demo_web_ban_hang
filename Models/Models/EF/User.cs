namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Feedbacks = new HashSet<Feedback>();
            Oders = new HashSet<Oder>();
            Avartar = "~/Content/upload/add.jpg";
        }

        public int ID { get; set; }
        [Display(Name = "Tên khách hàng")]
        [StringLength(250)]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(250)]
        public string Email { get; set; }
        [Display(Name = "SĐT")]
        [StringLength(250)]
        public string Phone { get; set; }
        [Display(Name = "Tài khoản")]
        [StringLength(250)]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [StringLength(250)]
        public string Pass { get; set; }

        [StringLength(250)]
        public string Avartar { get; set; }

        public bool? Status { get; set; }
        [NotMapped]
        public HttpPostedFileBase Imgfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oder> Oders { get; set; }
    }
}
