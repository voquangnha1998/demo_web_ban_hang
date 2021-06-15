using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using PagedList;

namespace Models.DAO
{
    public class UserDao
    {
        private WebDbContext db = null;
        public UserDao()
        {
            db = new WebDbContext();
        }
        public int login(string user, string pass)
        {
            var result = db.Admins.SingleOrDefault(x => x.UserName.Contains(user) && x.Pass.Contains(pass));
            
            //var result = db.Admins.Where(s => s.UserName.Equals(user) && s.Pass.Equals(pass)).ToList();
            if (result == null)
            {
                return 0;
            }
            else
            {

                return 1;
            }
        }
       

        public string Insert(User entityuser)
        {
            db.Users.Add(entityuser);
            db.SaveChanges();
            return entityuser.UserName;
        }

        public User Find(string username)
        {

            return db.Users.Find(username);
        }
        public List<User> ListAll()
        {
            return db.Users.ToList();
        }
        public IEnumerable<User> ListWhereAll(string keysearch,int page, int pagesize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model=model.Where(x => x.UserName.Contains(keysearch));
            }
           
            return model.OrderBy(x=>x.UserName).ToPagedList(page,pagesize);
        }


        //sản phẩm làm đây cho tiện !!!!!!!!!!!!
        public Product FindProduct(string proname)
        {

            return db.Products.Find(proname);
        }
        public List<Product> ListProductAll()
        {
            return db.Products.ToList();
        }
        /*public List<Product> ListProductWhereAll(string keysearch)
        {
            if (!string.IsNullOrEmpty(keysearch))
                return db.Products.Where(x => x.Name.Contains(keysearch)).ToList();
            return db.Products.ToList();
        }*/
        public IEnumerable<Product> ListProductWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.Name.Contains(keysearch));
            }

            return model.OrderBy(x => x.Name).ToPagedList(page, pagesize);
        }
        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }
    }
}
