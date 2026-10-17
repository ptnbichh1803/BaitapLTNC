using System;

namespace QuanLySinhVien_3Layer.Data.Entity
{
    public class SinhVien
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public decimal Diem { get; set; }
        public string TrangThai { get; set; }

        public SinhVien()
        {
        }

        public SinhVien(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, 
            string email, string dienThoai, string maLop, string tenLop, decimal diem, string trangThai)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Email = email;
            DienThoai = dienThoai;
            MaLop = maLop;
            TenLop = tenLop;
            Diem = diem;
            TrangThai = trangThai;
        }
    }
}
