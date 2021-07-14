using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
namespace WebSiteBanHang.Controllers
{
    public class DemoAjaxController : Controller
    {
        // GET: DemoAjax
        QuanLyBanHangEntities2 db = new QuanLyBanHangEntities2();
        public ActionResult DemoAjax()
        {
            return View();
        }
        // xu ly ajax acion link
        public ActionResult LoadAjaxActionLink()
        {
            System.Threading.Thread.Sleep(3000);
            return Content("Hello Ajax");
        }
        public ActionResult LoadAjaxBeginForm(FormCollection f)
        {
            string kq = f["txt1"].ToString();
            System.Threading.Thread.Sleep(3000);
            return Content(kq);
        }
        // xuly ajax jquery
        public ActionResult LoadAjaxJquery(int a,int b)
        {
            System.Threading.Thread.Sleep(3000);
            return Content((a+b).ToString());
        }
        // su dung ajax tra ve ket qua 1 partial view
       
        public ActionResult LoadSanPhamAjax()
        {
            // lay du lieu nap vao model laptop va moi
            // var lstSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            var lstSamPham = db.SanPham;
            return PartialView(lstSamPham); //(lstSanPhamLTM);
        }
    }
}