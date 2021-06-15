namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        public bool? Status { get; set; }

        public virtual User User { get; set; }
    }
}
