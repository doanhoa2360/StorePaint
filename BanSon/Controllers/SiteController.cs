using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using PagedList;
namespace BanSon.Controllers
{
    public class SiteController : Controller
    {

        private LinkDAO linkDAO = new LinkDAO();
        private ProductDAO productDAO = new ProductDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();
        private PostDAO postDAO = new PostDAO();
        private TopicDAO topicDAO = new TopicDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        // GET: Site
        public ActionResult Index(string slug = null)
        {
            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = linkDAO.getRow(slug);
                if (link != null)
                {
                    string typelink = link.Type;
                    switch (typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug);

                            }
                        case "topic":
                            {
                                return this.PostTopic(slug);

                            }
                        case "page":
                            {
                                return this.PostPage(slug);

                            }
                        case "supplier":
                            {
                                return this.ProductSupplier(slug);

                            }
                        default:
                            {
                                return this.Error404(slug);

                            }
                    }
                }
                else
                {
                    Product product = productDAO.getRow(slug);
                    if (product != null)
                    {
                        return this.ProductDetail(product);
                    }
                    else
                    {
                        Post post = postDAO.getRow(slug, "Post");
                        if (post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }
                }

            }

        }
        private ActionResult Home()
        {
           
            List<ProductInfo> productNew = productDAO.getList(4);
            ViewBag.ProductNew = productNew;
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("Home", list);
        }
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id);
            ViewBag.Category = category;
            List<int> listcatid = new List<int>();
            listcatid.Add(id);
            List<Category> listcategory2 = categoryDAO.getListByParentId(id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    foreach (var category3 in listcategory3)
                    {
                        listcatid.Add(category3.Id);
                    }
                }
            }
            List<ProductInfo> list = productDAO.getListByListCatId(listcatid, 4);
            return View("HomeProduct", list);
        }
        public ActionResult Product(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 9;
            IPagedList<ProductInfo> list = productDAO.getList(pageSize, pageNumber);
            return View("Product", list);
        }
        private ActionResult ProductCategory(string slug)
        {
            Category category = categoryDAO.getRow(slug);
            ViewBag.Category = category;
            List<int> listcatid = new List<int>();
            listcatid.Add(category.Id);
            List<Category> listcategory2 = categoryDAO.getListByParentId(category.Id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    foreach (var category3 in listcategory3)
                    {
                        listcatid.Add(category3.Id);
                    }
                }
            }

            int take = 10;
            List<ProductInfo> list = productDAO.getListByListCatId(listcatid, take);

            return View("ProductCategory", list);
        }
        private ActionResult ProductDetail(Product product)
        {
            ViewBag.ListOther = productDAO.getListByCatId(product.CatId, product.Id);
            return View("ProductDetail", product);
        } 
        public ActionResult ProductSupplier(string slug)
        {
            Supplier supplier = supplierDAO.getRow(slug);
            ViewBag.Supplier = supplier;
            List<Product> list = productDAO.getListBySupplierId(supplier.Id);
            return View("ProductSupplier");
        }
        public ActionResult Supplier(int? page)
        {
            List<Supplier> list = supplierDAO.getList();
            return View("Supplier", list);
        }
      
        public ActionResult Post(int? page)
        {

            List<PostInfo> list = postDAO.getList("Post");
            return View("Post", list);
        }

        private ActionResult PostTopic(string slug)
        {

            Topic topic = topicDAO.getRow(slug);
            ViewBag.Topic = topic;
            List<PostInfo> list = postDAO.getListByTopicId(topic.Id, "Post", null);
            return View("PostTopic", list);

        }
        private ActionResult PostPage(string slug)
        {

            Post post = postDAO.getRow(slug, "Page");
            return View("PostPage", post);
        }
        private ActionResult PostDetail(Post post)
        {
            ViewBag.ListOther = postDAO.getListByTopicId(post.TopicId, "Post", post.Id);
            return View("PostDetail", post);

        }

        private ActionResult Error404(string slug)
        {
            return View("Error404");
        }

    }
}