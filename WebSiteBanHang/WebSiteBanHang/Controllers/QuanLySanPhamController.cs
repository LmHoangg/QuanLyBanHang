using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        QuanLyBanHangEntities2 db = new QuanLyBanHangEntities2();
        public ActionResult Index()
        {
            return View(db.SanPham.Where(n=>n.DaXoa==false));
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            //load droplist nha cung cap
            ViewBag.MaNCC = new SelectList(db.NhaCungCap.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP=new SelectList(db.LoaiSanPham.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            return View();
        }
        [HttpPost]
        public ActionResult TaoMoi(HttpPostedFileBase HinhAnh)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCap.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPham.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            //kiểm tra tên hình xem đã tồn tại hay chưa.
            if (HinhAnh.ContentLength > 0)
            {   
                // lay ten hinh anh
                var fileName = Path.GetFileName(HinhAnh.FileName);
               /* lay hinh anh chuyen vao thu muc hinh anh*/
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), fileName);
                //neu thu muc chua hinh anh do roi xuat ra thong bao
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                }
                else
                {   
                    // lay hinh anh dua vao thu muc HinhAnh
                    HinhAnh.SaveAs(path);
                    
                }
            }
           
            return View();
        }
    }
}