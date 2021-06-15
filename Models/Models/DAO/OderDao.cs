using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OderDao
    {
        private WebDbContext db = null;
        public OderDao()
        {
            db = new WebDbContext();
        }
        public long Insert(Oder order)
        {
            db.Oders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        public List<Oder> ListAll()
        {
            return db.Oders.ToList();
        }
        public List<Oder> ListOderDaDuyet()
        {
            return db.Oders.Where(x=>x.Status==false).ToList();
        }
        public List<Oder> ListOderChuaDuyet()
        {
            return db.Oders.Where(x => x.Status == true).ToList();
        }
        public IEnumerable<Oder> ListProductWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<Oder> model = db.Oders;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.ShipName.Contains(keysearch));
            }

            return model.OrderBy(x => x.ShipName).ToPagedList(page, pagesize);
        }
    }
}
