using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models ;

namespace BanSon.Controllers
{
    public class ModuleController : Controller
    {
        private MenuDAO menuDAO = new MenuDAO();
        private SliderDAO sliderDAO = new SliderDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private PostDAO postDAO = new PostDAO();
        private UserDAO userDAO = new UserDAO();
        // GET: Module
        public ActionResult MainMenu()
        {
           List<Menu> list=menuDAO.getListByParentId("mainmenu",0);
            return View("MainMenu",list);
        }
        public ActionResult MainMenuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListByParentId("mainmenu", id);
            if (list.Count==0)
            {

                return View("MainMenuSub1", menu);
            }
            else
            {
                ViewBag.Menu = menu;
                return View("MainMenuSub2", list);
            }
        }
        public ActionResult Slideshow()
        {
            List<Slider> list = sliderDAO.getListByPosition("Slidershow");
            return View("Slideshow", list);
        }
        public ActionResult ListCategory()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("ListCategory", list);
        }
        public ActionResult PostLastNew()
        {
            List<Post> list = postDAO.getListByTopicId();
            return View("PostLastNew", list);
        }
        public ActionResult ListSupplier()
        {
            List<Supplier> list = supplierDAO.getList();
            return View("ListSupplier", list);
        }
        
        public ActionResult MenuFooter()
        {
            List<Menu> list = menuDAO.getListByParentId("footermenu", 0);
            return View("MenuFooter", list);
        }
        public ActionResult Account()
        {
            if (!Session["CustomerId"].Equals(""))
            {
                int userid = int.Parse(Session["CustomerId"].ToString());
                User user = userDAO.getRow(userid);
                ViewBag.User = user;
                return View("Account");
            }
            else
            {
                return View("Account");
            }
        }

    }
}