using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quanlysinhvien
{
    public class SinhVien
    {
        [Required]
        [RegularExpression("^SV[0-9]{6}$", ErrorMessage = "Mã sinh viên phải có định dạng SVxxxx, trong đó xxxx là 6 chữ số.")]
        public string MaSV { get; set; }
        [Range(5, 50, ErrorMessage = "Họ tên sinh viên phải có độ dài từ 5 đến 50 ký tự.")]
        public string HoTen { get; set; }
        [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Ngày sinh phải có định dạng dd/MM/yyyy.")]
        public DateTime NgaySinh { get; set; }
        [Required]
        [RegularExpression("^CSE[0-9]{4}$", ErrorMessage = "Mã lớp phải có định dạng CSExxxx, trong đó xxxx là 4 chữ số.")]
        public string MaLop { get; set; }
        [RegularExpression(@"^(Nam|Nữ|Khác)$", ErrorMessage = "Giới tính phải là 'Nam', 'Nữ' hoặc 'Khác'.")]
        public string GioiTinh { get; set; }
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Định dạng số điện thoại không hợp lệ.")]
        public string SoDienThoai { get; set; }
        [Required]
        public string TrangThai { get; set; }
        [Required]
        [RegularExpression(@"^(0(\.\d{1,2})?|10(\.0{1,2})?)$", ErrorMessage = "Điểm phải là số thực từ 0 đến 10 với tối đa 2 chữ số thập phân.")]
        public Double Diem { get; set; }
    }
}
