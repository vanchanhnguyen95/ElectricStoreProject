using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteDT.Models
{
    //khi cập nhật model nó ko bị mất
    [MetadataTypeAttribute(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetadata
        {
            ////Danh sách thuộc tính
            //[DisplayName("Mã thành viên")]
            //public int MaThanhVien { get; set; }
            //[DisplayName("Tài khoản")]
            //[Required(ErrorMessage = "{0} không được để trống!")]
            //public string TaiKhoan { get; set; }
            //[DisplayName("Mật khẩu")]
            //[Required(ErrorMessage = "{0} không được để trống!")]
            //public string MatKhau { get; set; }
            //[DisplayName("Họ Tên")]
            //[Required(ErrorMessage = "{0} không được để trống!")]
            //public string HoTen { get; set; }
            //[DisplayName("Địa chỉ")]
            //public string DiaChi { get; set; }
            //[DisplayName("Email")]
            //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            //ErrorMessage = "Email không đúng định dạng")]
            //public string Email { get; set; }
            //[DisplayName("Số điện thoại")]
            //public string SoDienThoai { get; set; }
            //[DisplayName("Câu hỏi")]
            //public string CauHoi { get; set; }
            //[DisplayName("Câu trả lời")]
            //public string CauTraLoi { get; set; }
            //[DisplayName("Mã loại thành viên")]
            //public Nullable<int> MaLoaiTV { get; set; }
        }
    }
}