using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    [MetadataTypeAttribute(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        internal sealed class ThanhVienMetadata
        {
            //Danh sách các thuộc tính
            [DisplayName("Mã thành viên")]
            public int MaThanhVien { get; set; }
            [DisplayName("Tài khoản")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string TaiKhoan { get; set; }
            [DisplayName("Mật khẩu")]
            [StringLength(15,ErrorMessage="Mật khẩu không quá 15 kí tự")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string MatKhau { get; set; }

            [DisplayName("Họ tên")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [StringLength(40, ErrorMessage = "Tên không quá 40 kí tự")]
            public string HoTen { get; set; }

            [DisplayName("Địa chỉ")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string DiaChi { get; set; }

            [DisplayName("Email")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email không hợp lệ!")]
            public string Email { get; set; }


            [DisplayName("Câu hỏi")]
            public string CauHoi { get; set; }

            [DisplayName("Câu trả lời")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string CauTraLoi { get; set; }

            [DisplayName("Số điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [Range(9,11,ErrorMessage="{0} phải từ {1} đến {2}")]
            public string SoDienThoai { get; set; }
            /* [DisplayName("Mã loại thành viên")]*/
        }
    }
}