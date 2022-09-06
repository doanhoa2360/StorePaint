using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanSon
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "TatCaSanPham",
             url: "tat-ca-san-pham",
             defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
         );
            routes.MapRoute(
          name: "TatCaBaiViet",
          url: "tat-ca-bai-viet",
          defaults: new { controller = "Site", action = "Post", id = UrlParameter.Optional }
      );
            routes.MapRoute(
    name: "TatCaThuongHieu",
    url: "tat-ca-thuong-hieu",
    defaults: new { controller = "Site", action = "Supplier", id = UrlParameter.Optional }
);
            routes.MapRoute(
                 name: "LienHe",
                url: "lien-he",
                defaults: new { controller = "LienHe", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "GioHang",
                url: "gio-hang",
                defaults: new { controller = "Giohang", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "ThanhToan",
            url: "thanh-toan",
            defaults: new { controller = "Giohang", action = "ThanhToan", id = UrlParameter.Optional }
            );
            routes.MapRoute(
         name: "ThanhCong",
         url: "thanh-cong",
         defaults: new { controller = "Giohang", action = "Thanhcong", id = UrlParameter.Optional }
     );
            routes.MapRoute(
                name: "DangNhap",
                url: "dang-nhap",
                defaults: new { controller = "KhachHang", action = "DangNhap", id = UrlParameter.Optional }
            );
            routes.MapRoute(
             name: "DangXuat",
             url: "dang-xuat",
             defaults: new { controller = "KhachHang", action = "DangXuat", id = UrlParameter.Optional }
         );
            routes.MapRoute(
      name: "DangKy",
      url: "dang-ky",
      defaults: new { controller = "KhachHang", action = "DangKy", id = UrlParameter.Optional }
);
            routes.MapRoute(
            name: "ThongTinKhachHang",
            url: "thong-tin-khach-hang",
            defaults: new { controller = "KhachHang", action = "ThongTinKhachHang", id = UrlParameter.Optional }
      );

            routes.MapRoute(
                  name: "TimKiem",
                  url: "tim-kiem",
                  defaults: new { controller = "TimKiem", action = "Index", id = UrlParameter.Optional }
              );
            routes.MapRoute(
               name: "SiteSlug",
               url: "{slug}",
               defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
