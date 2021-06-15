using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
   public class OderDetailDao
    {
        private WebDbContext db = null;
        public OderDetailDao()
        {
            db = new WebDbContext();
        }
        public bool Insert(OderDetail detail)
        {
            try
            {
                db.OderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
        public OderDetail ViewDetail(int orderId, int productId)
        {
            return db.OderDetails.Find(orderId, productId);
        }

        public List<OderDetail> ListOderDaDuyet()
        {
            return db.OderDetails.Where(x => x.Oder.Status == false).ToList();
        }
        public List<OderDetail> ListOderChuaDuyet()
        {
            return db.OderDetails.Where(x => x.Oder.Status == true).ToList();
        }
        public IEnumerable<Oder> ListOderWhereAll(string keysearch, int page, int pagesize)
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
