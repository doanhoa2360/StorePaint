using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using System.IO;

namespace BanSon.Areas.Admin.Controllers
{
    public class PageController : BaseController
    {
        private PostDAO postDAO = new PostDAO();
        private LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(postDAO.getList("Index", "Page"));
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Page";
                post.Slug = XString.Str_Slug(post.Title);
                post.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                post.CreatedAt = DateTime.Now;
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (FileExtentions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));

                        string pathDir = "~/Public/images/Pages/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        fileImg.SaveAs(pathImg);
                        post.Img = imgName;
                    }
                }
                if (postDAO.Insert(post) == 1)
                {
                    Link link = new Link();
                    link.Slug = post.Slug;
                    link.TableId = post.Id;
                    link.Type = "page";
                    linkDAO.Insert(link);
                }
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Page";
                post.Slug = XString.Str_Slug(post.Title);


                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (FileExtentions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/Pages/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        if (post.Img != null)
                        {
                            string pathImgDelete = Path.Combine(Server.MapPath(pathDir), post.Img);
                            System.IO.File.Delete(pathImgDelete);
                        }
                        fileImg.SaveAs(pathImg);
                        post.Img = imgName;
                    }
                    post.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                    post.UpdateAt = DateTime.Now;
                }
                if (postDAO.Update(post) == 1)
                {
                    Link link = linkDAO.getRow(post.Id, "page");
                    link.Slug = post.Slug;
                    linkDAO.Update(link);
                }
                postDAO.Update(post);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            return View(post);

        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDAO.getRow(id);
            postDAO.Delete(post);
            return RedirectToAction("Index");
        }
        public ActionResult Trash()
        {
            return View(postDAO.getList("Trash", "Page"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Page");

            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdateAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Page");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Page");

            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = 0;
            post.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdateAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Page");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Page");

            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            post.Status = 2;
            post.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdateAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Page");
        }
    }
}
