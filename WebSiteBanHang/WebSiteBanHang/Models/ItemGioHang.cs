using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class ItemGioHang
    {   
        public int MaSP { get; set; }
        public string TenSP { get; set; }
       
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }

        public ItemGioHang()
        {

        }
        public ItemGioHang(int iMaSP,int sl)
        {
            using (QuanLyBanHangEntities2 db = new QuanLyBanHangEntities2())
            {
                this.MaSP = iMaSP;
                SanPham sp = db.SanPham.Single(n => n.MaSP == iMaSP);
                this.TenSP = sp.TenSP;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGia.Value;
                this.SoLuong = sl;
                this.ThanhTien = DonGia * SoLuong;

            }
           
        }
        public ItemGioHang(int iMaSP)
        {
            using (QuanLyBanHangEntities2 db = new QuanLyBanHangEntities2())
            {
                this.MaSP = iMaSP;
                SanPham sp = db.SanPham.Single(n => n.MaSP == iMaSP);
                this.TenSP = sp.TenSP;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGia.Value;
                this.SoLuong = 1;
                this.ThanhTien = DonGia * SoLuong;

            }

        }
    }
}