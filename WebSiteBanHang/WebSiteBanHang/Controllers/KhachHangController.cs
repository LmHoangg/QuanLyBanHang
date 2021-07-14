using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
namespace WebSiteBanHang.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        QuanLyBanHangEntities2 db = new QuanLyBanHangEntities2();
        public ActionResult Index()
        {
            var listKH = from kh in db.KhachHang select kh;

            return View(listKH);
        }
        public ActionResult Index2()
        {
            var listKH = from kh in db.KhachHang select kh;

            return View(listKH);
        }
        public ActionResult TruyVan1DoiTuong()
        {
            var listKH = from k in db.KhachHang where k.MaKH==1 select k;
            //KhachHang kh = listKH.FirstOrDefault();
            KhachHang kh = db.KhachHang.SingleOrDefault(n => n.MaKH == 2);
            return View(kh);
        }
        public ActionResult SortDuLieu()
        {
            List<KhachHang> listKH = db.KhachHang.OrderBy(n => n.TenKH).ToList();
            return View(listKH);
        }
        public ActionResult GroupDuLieu()
        {
            List<ThanhVien> listKH = db.ThanhVien.OrderByDescending(n => n.TaiKhoan).ToList();
            return View(listKH);
        }
    }
}