using System;
using System.Collections.Generic;
using System.Linq;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer.Data.DAL
{
    public class LopDAL
    {
        private static List<Lop> _danhSachLop;

        static LopDAL()
        {
            _danhSachLop = new List<Lop>
            {
                new Lop("CNTT1", "Công nghệ thông tin 1"),
                new Lop("CNTT2", "Công nghệ thông tin 2"),
                new Lop("KTPM1", "Kỹ thuật phần mềm 1"),
                new Lop("HTTT1", "Hệ thống thông tin 1"),
                new Lop("KHMT1", "Khoa học máy tính 1")
            };
        }

        public List<Lop> GetAll()
        {
            return new List<Lop>(_danhSachLop);
        }

        public Lop GetById(string maLop)
        {
            return _danhSachLop.FirstOrDefault(l => l.MaLop.Equals(maLop, StringComparison.OrdinalIgnoreCase));
        }
    }
}
