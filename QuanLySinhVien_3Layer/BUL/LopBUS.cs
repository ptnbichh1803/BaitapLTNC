using System;
using System.Collections.Generic;
using QuanLySinhVien_3Layer.Data.DAL;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer.BUL
{
    public class LopBUS
    {
        private readonly LopDAL _lopDAL;

        public LopBUS()
        {
            _lopDAL = new LopDAL();
        }

        public List<Lop> GetAll()
        {
            return _lopDAL.GetAll();
        }

        public Lop GetById(string maLop)
        {
            return _lopDAL.GetById(maLop);
        }
    }
}
