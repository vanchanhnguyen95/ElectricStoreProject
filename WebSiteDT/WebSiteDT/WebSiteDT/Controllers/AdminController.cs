using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteDT.Models;

namespace WebSiteDT.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult Index()

        {
            ViewBag.TongDonDatHang = HttpContext.Application["TongDonDatHang"].ToString();//lấy số lượng người truy cập từ application đã được tạo
            ViewBag.SoNguoiDangOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();//lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            //ViewBag.DoanhThuThang = ThongKeDoanhThuThang();
            ViewBag.TongDDH = ThongKeDonHang();//Thống kê đơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê thành viên
            return View();
        }


        //Thống kê đơn hàng
        public double ThongKeDonHang()
        {
            //Đếm đơn đặt hàng
            double slddh = db.DonDatHangs.Count();
            return slddh;
        }

        //Thống kê thành viên
        public double ThongKeThanhVien()
        {
            //Đếm đơn đặt hàng
            double sltv = db.ThanhViens.Count();
            return sltv;
        }

        //Thống kê tổng doanh thu
        public decimal ThongKeTongDoanhThu()
        {
            //Thống kê theo tất cả doanh thu từ khi website thành lập
            decimal TongDoanhThu = db.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGia).Value;

            return TongDoanhThu;
        }

        public decimal ThongKeDoanhThuThang(int Thang, int Nam)
        {
            //List ra nhưng đơn đặt hàng nào có tháng, năm tương ứng
            var lstDDH = db.DonDatHangs.Where(n => n.NgayDat.Value.Month == Thang && n.NgayDat.Value.Year == Nam);
            decimal TongTien = 0;
            //Duyệt chi tiết của đơn đặt hàng đó và lấy tổng tiền của tất cả sản phẩm đơn hàng đó
            foreach (var item in lstDDH)
            {
                TongTien += decimal.Parse(item.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGia).Value.ToString());

            }
            return TongTien;


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}