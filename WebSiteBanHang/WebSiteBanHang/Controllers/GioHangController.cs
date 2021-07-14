using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangEntities2 db = new QuanLyBanHangEntities2();
        // lay gio hang 
        public List<ItemGioHang>LayGioHang()
            {
                // Gio hang da ton tai
                List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
                if(lstGioHang == null)
                {
                    // Neu gio hang chua ton tai thi khoi tao gio hang
                    lstGioHang = new List<ItemGioHang>();
                    Session["GioHang"] = lstGioHang;
                   /* return lstGioHang;*/
                }
                return lstGioHang;
            }
        // them gio hang thong thuong load lai trang

        public ActionResult ThemGioHang(int MaSP,string strUrl)
        {
            // kiem tra san pham ton tai trong csdl hay ko
            SanPham sp = db.SanPham.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lay gio hang
            List<ItemGioHang> lstGioHang = LayGioHang();
            // TH1 : san pham da ton tai trong gio hang
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if(spCheck != null)
            {   
                // Kiem Tra so luong ton truoc khi cho khac hang mua hang
                if(sp.SoLuongTon < spCheck.SoLuong)
                {
                    return View("ThongBao");
                }    
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strUrl);
            }
           
            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(itemGH);
            return Redirect(strUrl);
        }
        public double TinhTongSoLuong()
        {
            //Lay gio hang
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang> ;
            if(lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);


        }
        // Tinh Tong Tien
        public decimal TinhTongTien()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }

        // GET: GioHang
        public ActionResult XemGioHang()
        {
            List<ItemGioHang> lstGH = LayGioHang();

            return View(lstGH);
        }
        public ActionResult GioHangPartial()
        {
            
            if (TinhTongSoLuong() == 0)
             {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            else
            {
                ViewBag.TongSoLuong = TinhTongSoLuong();
                ViewBag.TongTien = TinhTongTien();
            }

            return PartialView();

        }
        // Chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang(int MaSP)
        {   
            // Kiểm tra session giỏ hàng tồn tại chưa
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // ktra sp ton tai
            SanPham sp = db.SanPham.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Lấy list giỏ hàng từ session
            List<ItemGioHang> lstGH = LayGioHang();
            // Ktra xem sp do ton tai trong gio hang hay khong?
            ItemGioHang spCheck = lstGH.SingleOrDefault(n => n.MaSP == MaSP);
            if(spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.GioHang = lstGH;
            // Neu ton tai roi
            return View(spCheck);
           
        }
        //Xu ly cap nhat gio hang
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH) //Thay cho viec truyen tham so so luong moi can cap nhat
        {
            //Kiểm tra số lượng tồn
            SanPham spCheck = db.SanPham.SingleOrDefault(n => n.MaSP == itemGH.MaSP);
            if(spCheck.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            // cập nhật số lương trong session giỏ hàng
            // b1: Lấy list gio hang từ session  gio hang
                List<ItemGioHang> lstGH = LayGioHang();
            //b2 Lấy sản phẩm cần cập nhật từ trong list<GioHang> ra
                ItemGioHang itemGHUpDate = lstGH.Find(n => n.MaSP == itemGH.MaSP);
            //b3  cập nhật lại số lượng thành tiền
            itemGHUpDate.SoLuong = itemGH.SoLuong;

            // tinh lai thanh tien
            itemGHUpDate.ThanhTien = itemGHUpDate.SoLuong * itemGHUpDate.DonGia;
                return RedirectToAction("XemGioHang");
        }


        public ActionResult XoaGioHang(int MaSP)
        {
            // Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // ktra sp ton tai
            SanPham sp = db.SanPham.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> lstGH = LayGioHang();
            // Ktra xem sp do ton tai trong gio hang hay khong?
            ItemGioHang spCheck = lstGH.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Xóa item trong giỏ hàng
            lstGH.Remove(spCheck);
            return RedirectToAction("XemGioHang");
        }
        // xay dung chuc nang Đặt Hàng
        public ActionResult DatHang(KhachHang kh)
        {
            // Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KhachHang KH= new KhachHang();
            if (Session["TaiKhoan"] == null)
            {
                // thêm khách hàng vào bảng khách hàng (khách hàng ko có tài khoản)
                 KH = new KhachHang();
                    KH = kh;
                db.KhachHang.Add(KH);
                db.SaveChanges();

            }
            else
            {   
                // đối vs khách hàng là thành viên
                ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
                KH.TenKH = tv.HoTen;
                KH.DiaChi = tv.DiaChi;
                KH.Email = tv.Email;
                KH.SoDienThoai = tv.SoDienThoai;
                KH.MaThanhVien = tv.MaLoaiTV;
                db.KhachHang.Add(KH);
                db.SaveChanges();
            }
            // Thêm đơn hàng
            DonDatHang ddh = new DonDatHang();

            ddh.MaKH = int.Parse(KH.MaKH.ToString());
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.UuDai = 0;
            ddh.DaHuy = false;
            ddh.DaXoa = false;
            db.DonDatHang.Add(ddh);
            db.SaveChanges();
            // Thêm chi tiết đơn hàng
            List<ItemGioHang> lst = LayGioHang();
            foreach(var item in lst)
            {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.TenSP = item.TenSP;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = item.DonGia;
                db.ChiTietDonDatHang.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
        }

        // Thêm giỏ hàng  = ajax
        public ActionResult ThemGioHangAjax(int MaSP, string strUrl)
        {
            // kiem tra san pham ton tai trong csdl hay ko
            SanPham sp = db.SanPham.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lay gio hang
            List<ItemGioHang> lstGioHang = LayGioHang();
            // TH1 : san pham da ton tai trong gio hang
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                // Kiem Tra so luong ton truoc khi cho khac hang mua hang
                if (sp.SoLuongTon < spCheck.SoLuong)
                {
                    return Content("<script> alert(\"Sản phẩm đã hết hàng !\")</script>");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                ViewBag.TongSoLuong = TinhTongSoLuong();
                ViewBag.TongTien = TinhTongTien();
                return PartialView("GioHangPartial");
            }

            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return Content("<script> alert(\"Sản phẩm đã hết hàng !\")</script>");
            }
            lstGioHang.Add(itemGH);
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView("GioHangPartial");
        }
    }
}