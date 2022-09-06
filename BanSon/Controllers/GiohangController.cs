using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace BanSon.Controllers
{
    public class GiohangController : Controller
    {
        OrderdetailDAO orderdetailDAO = new OrderdetailDAO();
        OrderDAO orderDAO = new OrderDAO();
        UserDAO userDAO = new UserDAO();
        ProductDAO productDAO = new ProductDAO();
        XCart xcart = new XCart();
        // GET: GioHang
        public ActionResult Index()
        {
            List<CartItem> listcart = xcart.getCart();
            return View("Index", listcart);
        }
        public ActionResult CartAdd(int productid)
        {
            Product product = productDAO.getRow(productid);
            CartItem cartitem = new CartItem(product.Id, product.Name, product.Img, product.PriceSale, 1);
            List<CartItem> listcart = xcart.AddCart(cartitem, productid);
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult CartDel(int productid)
        {
            xcart.DelCart(productid);
            return RedirectToAction("Index", "Giohang");
        }
        //CartUpdate
        public ActionResult CartUpdate(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["CAPNHAT"]))
            {
                var listqty = form["qty"];
                var listarr = listqty.Split(',');
                xcart.UpdateCart(listarr);

            }
            return RedirectToAction("Index", "Giohang");
        }
        //CartDelAll
        public ActionResult CartDelAll()
        {
            xcart.DelCart();
            return RedirectToAction("Index", "Giohang");
        }

        //ThanhToan
        public ActionResult ThanhToan()
        {
            List<CartItem> listcart = xcart.getCart();
            //kiểm tra đăng nhaaoj trang người dùng ==> khách hàng
            if (Session["UserCustomer"].Equals(""))
            {
                return RedirectToAction("DangNhap", "KhachHang"); // chuyển đến URL
            }
            int userid = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.getRow(userid);
            ViewBag.User = user;
            return View("ThanhToan", listcart);
        }
        public ActionResult DatMua(FormCollection form)
        {
            // Lưu thông tin vào CSDL vào Orders và OrderDetail
            int userid = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.getRow(userid);
            // Lấy thông tin
            string note = form["Note"];

            // Tạo đối tượng đơn hàng
            Order order = new Order();
            order.UserId = userid;
            order.CreatedAt = DateTime.Now;
            order.ReceiverAddress = form["ReceiverAddress"];
            order.ReceiverEmail = form["ReceiverEmail"];
            order.ReceiverPhone = form["ReceiverPhone"];
            order.ReceiverName = form["ReceiverName"];
            order.Note = note;
            order.Status = 1;
            if (orderDAO.Insert(order) == 1)
            {
                // Thêm vào chi tiết đơn hàng
                List<CartItem> listcart = xcart.getCart();
                foreach (CartItem cartItem in listcart)
                {
                    Orderdetail orderDetail = new Orderdetail();
                    orderDetail.OrderId = order.Id;
                    orderDetail.ProductId = cartItem.ProductId;
                    orderDetail.Price = cartItem.Price;
                    orderDetail.Qty = cartItem.Qty;
                    orderDetail.Amount = cartItem.Amount;
                    orderdetailDAO.Insert(orderDetail);
                }
            }
            return RedirectToAction("ThanhCong", "Giohang"); // chuyển đến URL
        }
        public ActionResult ThanhCong()
        {
            return View("ThanhCong");
        }
    }
}