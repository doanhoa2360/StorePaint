using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class ContactDAO
    {
        private MyDBContext db = new MyDBContext();
        //trả về danh sách các liên hệ
        public List<Contact> getList(string status = "All")
        {
            List<Contact> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Contacts.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Contacts.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Contacts.ToList();
                        break;
                    }
            }
            return list;
        }
        //Trả về 1 liên hệ
        public Contact getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Contacts.Find(id);
            }
        }
        //Thêm liên hệ
        public int Insert(Contact row)
        {
            db.Contacts.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật liên hệ
        public int Update(Contact row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa liên hệ

        public int Delete(Contact row)
        {
            db.Contacts.Remove(row);
            return db.SaveChanges();
        }
    }
}
