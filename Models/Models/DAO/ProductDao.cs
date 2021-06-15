using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ProductDao
    {
        private WebDbContext db = null;
        public ProductDao()
        {
            db = new WebDbContext();
        }
        // lấy list sản phẩm mới nhất
        public List<Product> ListNewProduct(int top)

        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<Product> GetListBanChay()

        {
            return db.Products.OrderByDescending(x => x.QuantitySold).Take(5).ToList();
        }
        public List<Product> ListProductIphone(int top)
        {
            /*List<Product> list = new List<Product>();
            var kq = from p in list
                      where p.CatProID==1
                      orderby p.CreateDate descending, p.Name
                      select p;

            return kq.ToList()*/;
            return db.Products.Where(x=>x.CatProID==1).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        //lấy tất cả sản phẩm bởi cái mã danh mục
        public List<Product> ListByCategoryID(long id)
        {
            return db.Products.Where(x => x.CatProID == id).ToList();
        }
    }
}
