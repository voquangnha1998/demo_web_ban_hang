using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Model
{
    public class LoginModel
    {
        [Required]
        [StringLength(250)]
        public string UserName { get; set; }

        [StringLength(250)]
        public string Pass { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public int ID { get; set; }
    }
}