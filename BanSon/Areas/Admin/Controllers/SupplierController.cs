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
    public class SupplierController : BaseController
    {
        SupplierDAO supplierDAO = new SupplierDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Supplier
        public ActionResult Index()
        {
            return View(supplierDAO.getList("Index"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.Slug = XString.Str_Slug(supplier.Name);
                if (supplier.Orders == null)
                {
                    supplier.Orders = 1;
                }
                else
                {
                    supplier.Orders += 1;

                }
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", "gif" };
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = supplier.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        supplier.Img = imgName;
                        string PathDir = "~/Public/images/Suppliers/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                supplier.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                supplier.CreatedAt = DateTime.Now;
                if (supplierDAO.Insert(supplier) == 1)
                {
                    Link link = new Link();
                    link.Slug = supplier.Slug;
                    link.TableId = supplier.Id;
                    link.Type = "supplier";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index", "Supplier");
            }
            ViewBag.ListCat = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCat = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.Slug = XString.Str_Slug(supplier.Name);
                if (supplier.Orders == null)
                {
                    supplier.Orders = 1;
                }
                else
                {
                    supplier.Orders += 1;

                }
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", "gif" };
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = supplier.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        supplier.Img = imgName;
                        string PathDir = "~/Public/images/Suppliers/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                supplier.UpdateAt = DateTime.Now;
                if (supplierDAO.Update(supplier) == 1)
                {
                    Link link = linkDAO.getRow(supplier.Id, "supplier");
                    link.Slug = supplier.Slug;
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = supplierDAO.getRow(id);
            Link link = linkDAO.getRow(supplier.Id, "supplier");
            if (supplierDAO.Delete(supplier) == 1)
            {
                linkDAO.Delete(link);
            }
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Trash", "Supplier");
        }
        public ActionResult Trash()
        {
            return View(supplierDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Supplier");

            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = (supplier.Status == 1) ? 2 : 1;
            supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Supplier");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Supplier");

            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = 0;
            supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Supplier");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Supplier");

            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Supplier");
            }
            supplier.Status = 2;
            supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Supplier");
        }
    }
}
