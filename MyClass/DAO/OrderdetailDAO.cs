using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class OrderdetailDAO
    {
        private MyDBContext db = new MyDBContext();
       public List<Orderdetail> getList(int? orderid)
        {
            return db.Orderdetails.Where(m=>m.OrderId==orderid).ToList();
        }
        public List<Orderdetail> getList(string status = "All")
        {
            List<Orderdetail> list=  db.Orderdetails.ToList();
            return list;
        }
        public Orderdetail getRow(int? id)
        {
            return db.Orderdetails.Find(id);
        }
        public int getCount()
        {
            return db.Orderdetails.Count();
        }
        //Thêm mẫu tin
        public int Insert(Orderdetail row)
        {
            db.Orderdetails.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Orderdetail row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Orderdetail row)
        {
            db.Orderdetails.Remove(row);
            return db.SaveChanges();
        }
    }
}
