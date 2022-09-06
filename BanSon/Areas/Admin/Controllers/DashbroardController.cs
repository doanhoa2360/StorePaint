using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanSon.Areas.Admin.Controllers
{
    public class DashbroardController : BaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
              
            return View();
        }
    }
}