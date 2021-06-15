namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data;
    using System.IO;
    using System.Web;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OderDetails = new HashSet<OderDetail>();
            Image = "~/Content/upload/addproduct.jpg";
        }

        public int ID { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [StringLength(250)]
        public string Name { get; set; }

        public int? CatProID { get; set; }
        [Display(Name = "Hình ảnh")]
        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }
        [Display(Name = "Giá bán")]
        public decimal? Price { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        public decimal? PriceKM { get; set; }
        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }

        [Display(Name = "Giá nhập vào")]
        public int? RemainingAmount { get; set; }
        [Display(Name = "Chi tiết")]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }
        [Display(Name = "Cấu hình")]
        [StringLength(250)]
        public string CauHinh { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Trạng thái")]
        public bool? Status { get; set; }
        [Display(Name = "Số lượng đã bán")]
        public int QuantitySold { get; set; }
        public virtual CategotyProduct CategotyProduct { get; set; }
        [NotMapped]
        public HttpPostedFileBase Imgfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OderDetail> OderDetails { get; set; }
    }
}
