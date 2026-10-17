using System;

namespace QuanLySinhVien_3Layer.Data.Entity
{
    public class Lop
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }

        public Lop()
        {
        }

        public Lop(string maLop, string tenLop)
        {
            MaLop = maLop;
            TenLop = tenLop;
        }

        public override string ToString()
        {
            return TenLop ?? string.Empty;
        }
    }
}
