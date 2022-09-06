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
    public class PostDAO
    {
        private MyDBContext db = new MyDBContext();
        //trả về danh sách các mẫu tin
        public List<Post> getListByTopicId(string type = "Post")
        {
            return db.Posts.Where(m => m.PostType == type && m.Status == 1).ToList();
        }
        public List<PostInfo> getListByTopicId(int? topid, string type = "Post", int? notid = null)
        {
            List<PostInfo> list = null;
            if (notid == null)
            {
                list = db.Posts.Join(
                db.Topics,
                p => p.TopicId,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    TopicId = p.TopicId,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    Metakey = p.Metakey,
                    MetaDesc = p.MetaDesc,
                    CreatedBy = p.CreatedBy,
                    CreatedAt = p.CreatedAt,
                    UpdateBy = p.UpdateBy,
                    UpdateAt = p.UpdateAt,
                    Status = p.Status
                })
        .Where(m => m.Status == 1 && m.PostType == type && m.TopicId == topid).ToList();
            }
            else
            {
                list = db.Posts.Join(
                               db.Topics,
                               p => p.TopicId,
                               t => t.Id,
                               (p, t) => new PostInfo
                               {
                                   Id = p.Id,
                                   TopicId = p.TopicId,
                                   TopicName = t.Name,
                                   Title = p.Title,
                                   Slug = p.Slug,
                                   Detail = p.Detail,
                                   Img = p.Img,
                                   PostType = p.PostType,
                                   Metakey = p.Metakey,
                                   MetaDesc = p.MetaDesc,
                                   CreatedBy = p.CreatedBy,
                                   CreatedAt = p.CreatedAt,
                                   UpdateBy = p.UpdateBy,
                                   UpdateAt = p.UpdateAt,
                                   Status = p.Status
                               })
                       .Where(m => m.Status == 1 && m.PostType == type && m.TopicId == topid && m.Id != notid).ToList();
            }
            return list;

        }
        public List<PostInfo> getList(string type = "Post")
        {
            List<PostInfo> list = db.Posts.Join(
                            db.Topics,
                            p => p.TopicId,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                TopicId = p.TopicId,
                                TopicName = t.Name,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                Metakey = p.Metakey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
                            })

                 .Where(m => m.Status == 1 && m.PostType == type)
                 .ToList();
            return list;
        }

        public List<Post> getList(string status = "All", string type = "Post")
        {
            List<Post> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Posts.Where(m => m.Status != 0 && m.PostType == type).OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Posts.Where(m => m.Status == 0 && m.PostType == type).OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Posts.Where(m => m.PostType == type).ToList();
                        break;
                    }
            }
            return list;
        }



        public List<PostInfo> getListJoin(string status = "All", string type="Post")
        {
            List<PostInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Posts.Join(
                            db.Topics,
                            p => p.TopicId,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                TopicId = p.TopicId,
                                TopicName = t.Name,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                Metakey = p.Metakey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
                            })
                            .Where(m => m.Status != 0 && m.PostType == type).OrderByDescending(m=>m.CreatedAt).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Posts
                             .Join(
                            db.Topics,
                            p => p.TopicId,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                TopicId = p.TopicId,
                                TopicName = t.Name,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                Metakey = p.Metakey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdateBy = p.UpdateBy,
                                UpdateAt = p.UpdateAt,
                                Status = p.Status
                            })
                            .Where(m => m.Status == 0 && m.PostType == type).OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Posts
                                     .Join(
                                    db.Topics,
                                    p => p.TopicId,
                                    t => t.Id,
                                    (p, t) => new PostInfo
                                    {
                                        Id = p.Id,
                                        TopicId = p.TopicId,
                                        TopicName = t.Name,
                                        Title = p.Title,
                                        Slug = p.Slug,
                                        Detail = p.Detail,
                                        Img = p.Img,
                                        PostType = p.PostType,
                                        Metakey = p.Metakey,
                                        MetaDesc = p.MetaDesc,
                                        CreatedBy = p.CreatedBy,
                                        CreatedAt = p.CreatedAt,
                                        UpdateBy = p.UpdateBy,
                                        UpdateAt = p.UpdateAt,
                                        Status = p.Status
                                    }
                            )
                            .Where(m => m.PostType == type).ToList();
                        break;
                    }
            }
            return list;
        }
        //Trả về 1 mẫu tin
        public Post getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Find(id);
            }
        }

        public Post getRow(string slug, string posttype)
        {

            return db.Posts.Where(m => m.Slug == slug && m.PostType == posttype && m.Status == 1).FirstOrDefault();

        }
        //Số mẫu tin
        public int getCount()
        {
            return db.Posts.Count();
        }
        //Thêm mẫu tin
        public int Insert(Post row)
        {
            db.Posts.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Post row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Post row)
        {
            db.Posts.Remove(row);
            return db.SaveChanges();
        }
    }
}

