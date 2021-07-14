using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using PagedList;
namespace WebSiteBanHang.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        QuanLyBanHangEntities2 db = new QuanLyBanHangEntities2();

        [HttpGet]
        public ActionResult KQTimKiem(string sTuKhoa,int? page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            // Thực hiên chức nâng phân trang
            // tạo biến số sản phẩm trên trang
            int PageSize = 6;
            // tạo biến số sô trang hiện tại
            int PageNumber = (page ?? 1);
            // phuong thuc tim kiem theo ten san pham
            var lstSP = db.SanPham.Where(n => n.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            return View(lstSP.OrderBy(n=>n.TenSP).ToPagedList(PageNumber,PageSize));
        }
        [HttpPost]
        public ActionResult LayTuKhoaTimKiem(string sTuKhoa)
        {
            // gọi về hàm get tìm kiếm
            return RedirectToAction("KQTimKiem", new { @sTuKhoa = sTuKhoa});
        }

        // Tim kiem = ajax
        public ActionResult KQTimKiemPartial(String sTuKhoa)
        {
            // Tìm kiếm theo tên sản phẩm
            var lstSP = db.SanPham.Where(n => n.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;

            return PartialView(lstSP.OrderBy(n=>n.DonGia));
        }
    }
}