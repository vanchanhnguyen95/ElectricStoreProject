﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebSiteDT.Models;

namespace WebSiteDT.Controllers
{
    public class QuanLyPhieuNhapController : Controller
    {
        // GET: QuanLyPhieuNhap
        private QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = db.NhaCungCaps;
            ViewBag.ListSanPham = db.SanPhams;
            return View();
        }

        [HttpPost]
        public ActionResult NhapHang(PhieuNhap model, IEnumerable<ChiTietPhieuNhap> lstModel)
        {
            ViewBag.MaNCC = db.NhaCungCaps;
            ViewBag.ListSanPham = db.SanPhams;
            //Sau khi các bạn đã kiểm tra tất cả dữ liệu đầu vào
            //Gán đã xóa: False
            model.DaXoa = false;
            db.PhieuNhaps.Add(model);
            db.SaveChanges();
            //SaveChanges để lấy được mã phiếu nhập gán cho lstChiTietPhieuNhap
            SanPham sp;
            foreach (var item in lstModel)
            {
                //Cập nhật số lượng tồn
                sp = db.SanPhams.Single(n => n.MaSP == item.MaSP);
                sp.SoLuongTon += item.SoLuongNhap;
                //Gán mã phiếu nhập cho tất cả chi tiết phiếu nhập
                item.MaPN = model.MaPN;
            }
            db.ChiTietPhieuNhaps.AddRange(lstModel);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult DSSPHetHang()
        {
            //Danh sách sản phẩm gần hết hàng với số lượng tồn bé hơn hoặc bằng 5
            var lstSP = db.SanPhams.Where(n => n.DaXoa == false && n.SoLuongTon <= 5);
            return View(lstSP);
        }

        //Tạo 1 view phục vụ cho việc nhập từng sản phẩm
        [HttpGet]
        public ActionResult NhapHangDon(int? id)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            //Tương tự như trang chỉnh sửa sản phẩm nhưng ta không cần phải show hết các thuộc tính
            //Chỉ thuộc tính nào cần thiết mà thôi đó là số lượng tồn hình ảnh... thông tin hiển thị cần thiết
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        // Xử lý nhập hàng từng sản phẩm
        [HttpPost]
        public ActionResult NhapHangDon(PhieuNhap model, ChiTietPhieuNhap ctpn)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", model.MaNCC);
            //Sau khi các bạn đã kiểm tra tất cả dữ liệu đầu vào
            //Gán đã xóa: False
            model.NgayNhap = DateTime.Now;
            model.DaXoa = false;
            db.PhieuNhaps.Add(model);
            db.SaveChanges();
            //SaveChanges để lấy được mã phiếu nhập gán cho lstChiTietPhieuNhap
            ctpn.MaPN = model.MaPN;
            //Cập nhật tồn
            SanPham sp = db.SanPhams.Single(n => n.MaSP == ctpn.MaSP);
            sp.SoLuongTon += ctpn.SoLuongNhap;
            db.ChiTietPhieuNhaps.Add(ctpn);
            db.SaveChanges();
            return View(sp);
        }

        //Giải phóng biến cho vùng nhớ
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