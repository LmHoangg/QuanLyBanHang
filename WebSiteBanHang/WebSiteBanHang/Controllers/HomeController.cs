using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;

namespace WebSiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyBanHangEntities2 db = new QuanLyBanHangEntities2();
        public ActionResult Index()
        {
            // lan luot tao view bag de lay list sanpham tu csdl
            //List dien thoai moi nhat
            var lstDTM = db.SanPham.Where(n => n.MaLoaiSP == 1 && n.DaXoa == false);
            // gan vao view bag
            ViewBag.listDTM = lstDTM;

            var lstLTM = db.SanPham.Where(n => n.MaLoaiSP == 2 && n.DaXoa == false);
            // gan vao view bag
            ViewBag.listLTM = lstLTM;
            var lstMTB = db.SanPham.Where(n => n.MaLoaiSP == 3 && n.DaXoa == false);
            // gan vao view bag
            ViewBag.listMTB = lstMTB;
            return View();
        }


        public ActionResult MenuPartial()
        {   
            // Truy van ve 1 list cac san pham
            var lstSanPham = db.SanPham;
            return PartialView(lstSanPham);
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());


            return View();
        }
        

        [HttpPost]
        public ActionResult DangKy(ThanhVien tv)
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            // them khach hang vao co so du lieu
            //ktra hop le catcha
            if (this.IsCaptchaValid("Captcha is not valid"))
            {   
               if(ModelState.IsValid)
                {
                 ViewBag.ThongBao="Thêm thành công";
                 db.ThanhVien.Add(tv);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.ThongBao = "Them That Bai !";
                }

                return View();
                
            }
            else
            {
                ViewBag.ThongBao = "Sai mã captcha";
            }
           return View();
           
        }
        public List<string> LoadCauHoi()
        {
            List<string> lstCauHoi = new List<string>();
            lstCauHoi.Add("Con vật mà bạn yêu thích?");
            lstCauHoi.Add("Ca sĩ mà bạn yêu thích?");
            lstCauHoi.Add("Hiện tại bạn đang làm công việc gì?");
            return lstCauHoi;
        }
        // Xay dung action dang nhap
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            // kiem tra ten dang nhap va mat khau

            string sTaiKhoan = f["txtTenDangNhap"].ToString();
            string sMatKhau = f["txtMatKhau"].ToString();
            ThanhVien tv = db.ThanhVien.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
                
                if (tv != null)
                {
                    Session["TaiKhoan"] = tv;
                /*return RedirectToAction("Index");*/
                return Content("<script>window.location.reload();</script>");
                }
            return Content("Tài Khoản hoặc Mật khẩu ko đúng!");
           /* return RedirectToAction("Index");*/
        }
       /* [ChildActionOnly]*/
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DangKy1()
        {
            /*ViewBag.CauHoi = new SelectList(LoadCauHoi());*/


            return View();
        }
        [HttpPost]
        public ActionResult DangKy1(ThanhVien tv)
        {
            /**/


            return View();
        }
    }
}