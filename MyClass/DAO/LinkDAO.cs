using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class LinkDAO
    {
        private MyDBContext db = new MyDBContext();
        public Link getRow(int? id)
        {
            return db.Links.Find(id);
        }

        public Link getRow(string slug)
        {
            return db.Links.Where(m => m.Slug == slug).FirstOrDefault();
        }
        public Link getRow(int tableid, string type)
        {
            return db.Links.Where(m => m.TableId == tableid && m.Type == type).FirstOrDefault();
        }
        //Thêm liên kết
        public int Insert(Link row)
        {
            db.Links.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật liên kết
        public int Update(Link row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa liên kết
        public int Delete(Link row)
        {
            db.Links.Remove(row);
            return db.SaveChanges();
        }
    }
}
