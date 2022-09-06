using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class UserDAO
    {
        private MyDBContext db = new MyDBContext();
        public List<User> getList(string status = "All")
        {
            List<User> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Users.Where(m => m.Status != 0).OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Users.Where(m => m.Status == 0).OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Users.ToList();
                        break;
                    }
            }
            return list;
        }
        //tra ve 1 mau tin

        public User getRow(string username, string roles)
        {
            if (username == null)
            {
                return null;
            }
            else
            {
                return db.Users.Where(m => m.Status == 1 && m.Roles == roles && (m.UserName == username || m.Email == username)).FirstOrDefault();
            }
        }

        public User getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Users.Find(id);
            }
        }
        //them mau tin
        public int Insert(User row)
        {
            db.Users.Add(row);
            return db.SaveChanges();
        }
        //Cap nhat mau tin
        public int Update(User row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Cap nhat mau tin
        public int Delete(User row)
        {
            db.Users.Remove(row);
            return db.SaveChanges();
        }

        public bool CheckUserName(string userName)
        {
            return db.Users.Count(m => m.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Users.Count(m => m.Email == email) > 0;
        }
    }
}
