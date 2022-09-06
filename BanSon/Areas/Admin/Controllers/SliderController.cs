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
    public class SliderController : BaseController
    {
        private SliderDAO sliderDAO = new SliderDAO();
        void SuccessNotifical(string s)
        {
            TempData["message"] = new XMessage("success", s + " thành công");
        }
        void ItemNotExist()
        {
            TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
        }

        // GET: Admin/Slider
        public ActionResult Index()
        {
            return View(sliderDAO.getList("Index"));
        }
        public ActionResult Trash()
        {
            return View(sliderDAO.getList("Trash"));
        }

        // GET: Admin/Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                ItemNotExist();
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                ItemNotExist();
                return RedirectToAction("Index", "Slider");
            }
            return View(slider);
        }

        // GET: Admin/Slider/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                //xử lý tên Slider khi nhập vào
                string names = slider.Name;
                names = names.First().ToString().ToUpper() + names.Substring(1);

                string link = XString.Str_Slug(slider.Name);
                slider.Url = link;

                slider.Name = names;
                slider.Position = "Slidershow";


                if (slider.Orders == null)
                {
                    slider.Orders = 0;
                }
                else
                {
                    slider.Orders += 1;
                }

                // xử lý hình ảnh
                var Img = Request.Files["FileImg"];
                string[] fileExtention = { ".jpg", ".png", ".gif" };
                if (Img.ContentLength != 0)
                {
                    //string time = DateTime.Now.ToString();
                    if (fileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        string imgName = link + "-" + Img.FileName.Substring(Img.FileName.LastIndexOf("."));

                        //lưu vào csdl 
                        slider.Img = imgName;

                        //lưu lên server
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Sliders/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                }
                slider.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                slider.CreatedAt = DateTime.Now;
                sliderDAO.Insert(slider);
                SuccessNotifical("Thêm");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }
        // POST: Admin/Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                //xử lý tên Slider khi nhập vào
                string names = slider.Name;
                names = names.First().ToString().ToUpper() + names.Substring(1);

                // lưu lại tên sau khi xử lý
                slider.Name = names;
                slider.Position = "Slidershow";
                // xử lý Slug || Links nếu có
                string link = XString.Str_Slug(slider.Name);
                slider.Url = link;
                if (slider.Orders == null)
                {
                    slider.Orders = 0;
                }
                else
                {
                    slider.Orders += 1;
                }

                // xử lý hình ảnh
                var Img = Request.Files["FileImg"];
                string[] fileExtention = { ".jpg", ".png", ".gif" };
                if (Img.ContentLength != 0)
                {
                    // nếu trong hình upload có chứa dấu . ==> thì cắt chuỗi + ráp với tên + thời gian
                    if (fileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        string imgName = link + "-" + Img.FileName.Substring(Img.FileName.LastIndexOf("."));

                        //kiểm tra tồn tại của hình// nếu không rỗng thì xóa
                        if (slider.Img != null)
                        {
                            // xóa hình
                            string Delpath = Path.Combine(Server.MapPath("~/Public/images/Sliders/"), slider.Img);
                            if (System.IO.File.Exists(Delpath))
                            {
                                System.IO.File.Delete(Delpath);
                            }
                        }
                        //lưu vào csdl 
                        slider.Img = imgName;

                        //lưu lên server
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Sliders/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                }

                slider.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                slider.UpdateAt = DateTime.Now;
                sliderDAO.Update(slider);
                SuccessNotifical("Cập nhật");
                return RedirectToAction("Index");
            }
                ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }


        // GET: Admin/Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                ItemNotExist();
                return RedirectToAction("Trash", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                ItemNotExist();
                return RedirectToAction("Trash", "Slider");
            }
            return View(slider);
        }

        // POST: Admin/Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = sliderDAO.getRow(id);
            //kiểm tra tồn tại của hình// nếu không rỗng thì xóa
            if (slider.Img != null)
            {
                // xóa hình
                string Delpath = Path.Combine(Server.MapPath("~/Public/images/Slider/"), slider.Img);
                if (System.IO.File.Exists(Delpath))
                {
                    System.IO.File.Delete(Delpath);
                }
            }
            sliderDAO.Delete(slider);
            SuccessNotifical("Xóa khỏi cơ sở dữ liệu");
            return RedirectToAction("Index");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                ItemNotExist();
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                ItemNotExist();
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = (slider.Status == 1) ? 2 : 1;


            slider.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            slider.UpdateAt = DateTime.Now;
            sliderDAO.Update(slider);
            SuccessNotifical("Cập nhật trạng thái");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                ItemNotExist();
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                ItemNotExist();
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = 0;
            sliderDAO.Update(slider);
            SuccessNotifical("Xóa vào thùng rác");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                ItemNotExist();
                return RedirectToAction("Trash", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                ItemNotExist();
                return RedirectToAction("Trash", "Slider");
            }
            slider.Status = 2;
            slider.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            slider.UpdateAt = DateTime.Now;
            sliderDAO.Update(slider);
            SuccessNotifical("Khôi phục về danh sách");
            return RedirectToAction("Trash", "Slider");
        }
    }
}

