using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
using PagedList;

namespace MyClass.DAO
{
    public class ProductDAO
    {
        private MyDBContext db = new MyDBContext();
        //trả về danh sách các mẫu tin
        public IPagedList<ProductInfo> getListByListCatId(List<int> listcatid, int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list= db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                SupplierId=p.SupplierId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
                            })

                .Where(m=>m.Status == 1 && listcatid.Contains(m.CatId))
                .OrderByDescending(m=>m.CreatedAt)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        public List<ProductInfo> getListByListCatId(List<int> listcatid, int take)
        {
            List<ProductInfo> list = db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                SupplierId = p.SupplierId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
                            })

                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.CreatedAt)
                .Take(take)
                .ToList();
            return list;
        }
        public List<ProductInfo> getListByCatId(int? catid,  int? notid = null)
        {
            List<ProductInfo> list = null;
            if (notid == null)
            {
                list = db.Products.Join(
                             db.Categorys,
                             p => p.CatId,
                             c => c.Id,
                             (p, c) => new ProductInfo
                             {
                                 Id = p.Id,
                                 CatId = p.CatId,
                                 SupplierId = p.SupplierId,
                                 Name = p.Name,
                                 CategoryName = c.Name,
                                 Slug = p.Slug,
                                 Detail = p.Detail,
                                 Img = p.Img,
                                 Price = p.Price,
                                 PriceSale = p.PriceSale,
                                 Number = p.Number,
                                 MetaKey = p.MetaKey,
                                 MetaDesc = p.MetaDesc,
                                 CreatedBy = p.CreatedBy,
                                 CreatedAt = p.CreatedAt,
                                 UpdateBy = p.UpdateBy,
                                 UpdateAt = p.UpdateAt,
                                 Status = p.Status
                             })
        .Where(m => m.Status == 1 && m.CatId == catid).ToList();
            }
            else
            {
               list = db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                SupplierId = p.SupplierId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
    })
                       .Where(m => m.Status == 1  && m.CatId == catid && m.Id != notid).ToList();
            }
            return list;

        }
        public List<ProductInfo> getList(int take)
        {
            List<ProductInfo> list = db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                SupplierId = p.SupplierId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
                            })

                .Where(m => m.Status == 1 )
                .OrderByDescending(m => m.CreatedAt)
                .Take(take)
                .ToList();
            return list;
        }
        public IPagedList<ProductInfo> getList(int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                SupplierId = p.SupplierId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
                            })

                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .ToPagedList(pageNumber,pageSize);
            return list;
        }
        public List<Product> getList(string status = "All")
        {
            List<Product> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Products.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Products.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Products.ToList();
                        break;
                    }
            }
            return list;
        }
        public List<ProductInfo> getListJoin(string status = "All")
        {
            List<ProductInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                SupplierId = p.SupplierId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
                            }

                            ).Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                SupplierId = p.SupplierId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
                            }

                            ).Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                SupplierId = p.SupplierId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
                            }

                            ).ToList();
                        break;
                    }
            }
            return list;
        }
        public List<Product> getListBySupplierId(int supplierid)
        {
            return db.Products.Where(m => m.Status == 1 && m.SupplierId == supplierid).ToList();
        }
        //Trả về 1 mẫu tin
        public Product getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }
        public Product getRow(string slug)
        {
            return db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();

        }
        //Thêm mẫu tin
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}
