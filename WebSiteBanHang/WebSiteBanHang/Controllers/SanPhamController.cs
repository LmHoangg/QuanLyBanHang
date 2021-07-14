using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using PagedList;
namespace WebSiteBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        QuanLyBanHangEntities2 db = new QuanLyBanHangEntities2();
       
        public ActionResult SanPham(int? MaLoaiSP,int? MaNSX,int? page)
        {
            if(Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");

            }
            if (MaLoaiSP == null || MaNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = db.SanPham.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaNSX);
                if (lstSP.Count() == 0)
                    {
                        return HttpNotFound();
                    }
                if(Request.HttpMethod != "GET")
                    {
                        page = 1;
                    }
            // Thực hiên chức nâng phân trang
            // tạo biến số sản phẩm trên trang
            int PageSize = 6;
            // tạo biến số sô trang hiện tại
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;
            return View(lstSP.OrderBy(n=>n.MaSP).ToPagedList(PageNumber,PageSize));
        }
        public ActionResult SanPham1()
        {
            var lstSanPhamLTM = db.SanPham.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            ViewBag.ListSP = lstSanPhamLTM;


            var lstSanPhamDTM = db.SanPham.Where(n => n.MaLoaiSP == 1);
            ViewBag.ListDT = lstSanPhamDTM;

            var lstSanPhamMTBM = db.SanPham.Where(n => n.MaLoaiSP==3);
            ViewBag.ListMTB = lstSanPhamMTBM;

            return View();
        }
        public ActionResult SanPham2()
        {
            var lstSanPhamLTM = db.SanPham.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            ViewBag.ListSP = lstSanPhamLTM;

            var lstSanPhamDTM = db.SanPham.Where(n => n.MaLoaiSP == 1);
            ViewBag.ListDT = lstSanPhamDTM;


            return View();
        }
        // tao partial
        [ChildActionOnly]
        public ActionResult SanPhamStyle1Partial()
        {
            // lay du lieu nap vao model laptop va moi
           // var lstSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);

            return PartialView();//(lstSanPhamLTM);
        }
        /// <summary>
        ///  Xay dung trang web;
        /// </summary>
        [ChildActionOnly]
        public ActionResult SanPhamStyle2Partial()
        {
            return PartialView();
        }

        // XAY DUNG TRANG XEM CHI TIET

        public ActionResult XemChiTiet(int? id,string tensp)
        {   
            // kiem tra tham so truyen vao co rong hay khong ?
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            } // Neu khong thi truy xuat csdl lay ra san pham tuong ung
            SanPham sp = db.SanPham.SingleOrDefault(n => n.MaSP == id && n.DaXoa==false);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
    }
}