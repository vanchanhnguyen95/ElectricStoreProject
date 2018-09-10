using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteDT.Models;

namespace WebSiteDT.Controllers
{
    public class QuanLyThanhVienController : Controller
    {
        // GET: QuanLyThanhVien
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult Index()
        {
            //return View(db.SanPhams.Where(n => n.DaXoa == false).OrderByDescending(n => n.MaSP));
            return View(db.ThanhViens.OrderBy(n => n.HoTen));
        }

        //Load câu hỏi bí mật
        public List<string> LoadCauHoi()
        {
            List<string> lstCauHoi = new List<string>();
            lstCauHoi.Add("Con vật mà bạn yêu thích?");
            lstCauHoi.Add("Ca sĩ mà bạn yêu thích?");
            lstCauHoi.Add("Hiện tại bạn đang làm công việc gì?");
            return lstCauHoi;
        }

        [HttpGet]
        public ActionResult ThemThanhVien()
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            return View();
        }
        [HttpPost]
        public ActionResult ThemThanhVien(ThanhVien tv)
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            if (ModelState.IsValid)
            {
                db.ThanhViens.Add(tv);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //Chỉnh sửa thành viên
        [HttpGet]
        public ActionResult ChinhSuaThanhVien(int? id)
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            //Lấy thành viên cần chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.MaThanhVien == id);
            if (tv == null)
            {
                return HttpNotFound();
            }

            return View(tv);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSuaThanhVien(ThanhVien thanhVien)
        {
            //Nếu dữ liệu đầu vào chắn chắn ok 
            db.Entry(thanhVien).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult XoaThanhVien(int? id)
        {

            //Lấy thành viên cần chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.MaThanhVien == id);
            if (tv == null)
            {
                return HttpNotFound();
            }
            return View(tv);
        }

        [HttpPost]
        public ActionResult XoaThanhVien(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhViens.SingleOrDefault(n => n.MaThanhVien == id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            db.ThanhViens.Remove(thanhVien);
            db.SaveChanges();
            return RedirectToAction("Index");
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