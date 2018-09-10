using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSiteDT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Cấu hình đường dẫn (ko tham số) cho trang đăng ký: của controller Home
            routes.MapRoute(
                name: "DangKy",
                url: "tao-tai-khoan",
                defaults: new { controller = "Home", action = "DangKy", id = UrlParameter.Optional }
            );

            //Cấu hình đường dẫn (ko tham số) cho trang giới thiệu: của controller Home
            routes.MapRoute(
                name: "AboutUs",
                url: "gioi-thieu",
                defaults: new { controller = "Home", action = "AboutUs", id = UrlParameter.Optional }
            );

            //Cấu hình đường dẫn (ko tham số) cho trang liên hệ: của controller Home
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );

            //Cấu hình đường dẫn ko tham số: của controller Home
            routes.MapRoute(
                name: "TrangChu",
                url: "Trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //Cấu hình đường dẫn (ko tham số) cho trang xem giỏ hàng: của controller GioHang
            routes.MapRoute(
                name: "XemGioHang",
                url: "xem-gio-hang",
                defaults: new { controller = "GioHang", action = "XemGioHang", id = UrlParameter.Optional }
            );

            //Cấu hình có tham số: trang xem chi tiết của controller sản phẩm
            routes.MapRoute(
                name: "XemChiTiet",
                url: "{tensp}-{id}",
                defaults: new { controller = "SanPham", action = "XemChiTiet", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
