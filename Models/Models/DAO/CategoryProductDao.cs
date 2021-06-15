using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CategoryProductDao
    {
        private WebDbContext db = null;
        public CategoryProductDao()
        {
            db = new WebDbContext();
        }
        // lấy list sản phẩm mới nhất
        public List<CategotyProduct> ListCategoryProduct()
        {
            return db.CategotyProducts.ToList();
        }
        public CategotyProduct ViewDetail(long id)
        {
           return db.CategotyProducts.Find(id);
        }
        public List<CategotyProduct> ListAll()
        {
            return db.CategotyProducts.ToList();
        }
    }
}
