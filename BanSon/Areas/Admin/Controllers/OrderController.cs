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

namespace BanSon.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        OrderDAO orderDAO = new OrderDAO();
        OrderdetailDAO orderdetailDAO = new OrderdetailDAO();
        // GET: Admin/Order
        public ActionResult Index()
        {
            List<Order> list = orderDAO.getList("Index");
            return View(list);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListChiTiet = orderdetailDAO.getList(id);
            return View(order);
        }

        // GET: Admin/Order/Create

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow(id);
            orderDAO.Delete(order);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Trash", "Order");
        }
        public ActionResult Trash()
        {
            return View(orderDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Order");

            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = (order.Status == 1) ? 2 : 1;
            order.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdateAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Order");

            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = 0;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Order");

            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Order");
            }
            order.Status = 2;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Order");
        }
        public ActionResult Destroy(int? id)
        {

            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            if (order.Status == 1 || order.Status == 2)
            {
                order.Status = 0;
                order.UpdateAt = DateTime.Now;
                order.UpdateBy = 1;
            }
            else
            {
                if (order.Status == 3)
                {
                TempData["message"] = new XMessage("danger", "Đơn hàng đã vận chuyển không thể hủy");

                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã giao không thể hủy");

                }
                return RedirectToAction("Index", "Order");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Hủy đơn hàng thành công");
            return RedirectToAction("Index", "Order");
        }
    }
}
