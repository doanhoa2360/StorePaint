using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace BanSon.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private ProductDAO productDAO = new ProductDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();
        // GET: Admin/Product
        public ActionResult Index()
        {
            List<ProductInfo> list = productDAO.getListJoin("Index");
            return View("Index", list);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                product.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                product.CreatedAt = DateTime.Now;
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (FileExtentions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));

                        string pathDir = "~/Public/images/Products/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        fileImg.SaveAs(pathImg);
                        product.Img = imgName;
                    }
                }
                productDAO.Insert(product);
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index", "Product");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                product.UpdateAt = DateTime.Now;
                product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (FileExtentions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/Products/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        if (product.Img != null)
                        {
                            string pathImgDelete = Path.Combine(Server.MapPath(pathDir), product.Img);
                            System.IO.File.Delete(pathImgDelete);
                        }
                        fileImg.SaveAs(pathImg);
                        product.Img = imgName;
                    }
                }
                productDAO.Update(product);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Trash", "Product");
        }
        public ActionResult Trash()
        {
            List<ProductInfo> list = productDAO.getListJoin("Trash");
            return View("Trash", list);
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");

            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");

            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0;
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");

            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2;
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Khôi phục vào danh mục thành công");
            return RedirectToAction("Trash", "Product");
        }
        public String ProductImg(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Img;
            }
        }
        public String ProductName(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Name;
            }
        }
    }
}
