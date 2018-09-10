using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteDT.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using System.Web.Security;
using SweetAlert.Controllers;

namespace WebSiteDT.Controllers
{
    
    public class HomeController : Controller
    {
        

        // GET: Home
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        public ActionResult Index()
        {
            //Lần lượt tạo các ViewBac để lấy list sản phẩm từ cơ sở dũ liệu
            //List Điện thoại mới
            var lstDTM = db.SanPhams.Where(n => n.MaLoaiSP == 1 && n.Moi == 1 && n.DaXoa == false).ToList();
            //Gắn vào ViewBag
            ViewBag.ListDTM = lstDTM;

            //List Laptop mới
            var lstLTM = db.SanPhams.Where(n => n.MaLoaiSP == 3 && n.Moi == 1 && n.DaXoa == false).ToList();
            //Gắn vào ViewBag
            ViewBag.ListLTM = lstLTM;

            //List MÁy tính bảng mới
            var lstMTBM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1 && n.DaXoa == false).ToList();
            //Gắn vào ViewBag
            ViewBag.ListlstMTBM = lstMTBM;

            return View();
        }
        
        //Tạo menu động
        public ActionResult MenuPartial()
        {
            //Truy vấn lấy về 1 list các sản phẩm
            var lstSP = db.SanPhams;
            return View(lstSP);
        }

        //Tạo menu động
        public ActionResult CategoryPartial()
        {
            //Truy vấn lấy về 1 list các sản phẩm
            var lstSP = db.SanPhams;
            return View(lstSP);
        }

        [HttpGet]
        //Tạo Action Đăng ký
        public ActionResult DangKy()
        {
            //Load câu hỏi ra, dùng ViewBag lấy giá trị các câu hỏi
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(ThanhVien tv, FormCollection f)
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            tv.MaLoaiTV = 2;
            //Kiểm tra captcha hợp lệ
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ThongBao = "Đăng ký thành công";
                    //Thêm khách hàng vào csdl
                    db.ThanhViens.Add(tv);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ThongBao = "Thêm thất bại";

                }
                return View();
            }
            TempData["Message"] = "Message: blahblah";
            ViewBag.ThongBao = "Sai mã captcha";


            return View();
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
        [HttpPost]
        //Xây dựng Action đăng nhập
        public ActionResult DangNhap(FormCollection f)
        {
            ////Kiểm tra tên đăng nhập và mật khẩu
            
            string taikhoan = f["txtTaiKhoan"].ToString();
            string matkhau = f["txtMatKhau"].ToString();
            string urllink = f["redirec"].ToString();
            //Truy vấn kiểm tra đăng nhập lấy thông tin thành viên
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == taikhoan && n.MatKhau == matkhau);
            if (tv != null)
            {
                //Lấy ra list quyền của thành viên tương ứng với loại thành viên
                var lstQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLoaiTV == tv.MaLoaiTV);
                //Duyệt list quyền
                string Quyen = "";
                if (lstQuyen.Count() != 0)
                {
                    foreach (var item in lstQuyen)
                    {
                        Quyen += item.Quyen.MaQuyen + ",";
                    }
                    Quyen = Quyen.Substring(0, Quyen.Length - 1); //Cắt dấu ","
                    PhanQuyen(tv.TaiKhoan.ToString(), Quyen);
                    Session["TaiKhoan"] = tv;
                    //return Content("<script>window.location.reload();</script>");
                    return Redirect(urllink);
                }
            }
            TempData["LoiDangNhap"] = "Đăng nhập không đúng!";
            return Redirect(urllink);

        }

        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                          TaiKhoan, //user
                                          DateTime.Now, //Thời gian bắt đầu
                                          DateTime.Now.AddHours(3), //Thời gian kết thúc
                                          false, //Ghi nhớ?
                                          Quyen, // "DangKy,QuanLyDonHang,QuanLySanPham"
                                          FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        //Tạo trang ngăn chặn quyền truy cập
        public ActionResult LoiPhanQuyen()
        {

            return View();
        }

        //Xây dụng Action đăng xuất
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}