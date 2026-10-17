using System;
using System.Collections.Generic;
using QuanLySinhVien_3Layer.Data.DAL;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer.BUL
{
    /// <summary>
    /// Tầng nghiệp vụ cho Lớp học (Class).
    /// Các phương thức ném ngoại lệ khi có lỗi để tầng View xử lý.
    /// </summary>
    public class LopBUS
    {
        private readonly LopHocDAO _lopDAO;

        public LopBUS()
        {
            _lopDAO = new LopHocDAO();
        }

        public List<Lop> GetAll()
        {
            return _lopDAO.GetAll();
        }

        public Lop GetById(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new ArgumentException("Mã lớp không được để trống.", "maLop");

            return _lopDAO.GetById(maLop);
        }

        public void Them(Lop lop)
        {
            if (lop == null)
                throw new ArgumentNullException("lop", "Thông tin lớp học không hợp lệ.");
            if (string.IsNullOrWhiteSpace(lop.MaLop))
                throw new ArgumentException("Mã lớp không được để trống.");
            if (string.IsNullOrWhiteSpace(lop.TenLop))
                throw new ArgumentException("Tên lớp không được để trống.");

            _lopDAO.Add(lop);
        }

        public void Sua(Lop lop)
        {
            if (lop == null)
                throw new ArgumentNullException("lop", "Thông tin lớp học không hợp lệ.");
            if (string.IsNullOrWhiteSpace(lop.MaLop))
                throw new ArgumentException("Mã lớp không được để trống.");
            if (string.IsNullOrWhiteSpace(lop.TenLop))
                throw new ArgumentException("Tên lớp không được để trống.");

            _lopDAO.Update(lop);
        }

        public void Xoa(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new ArgumentException("Mã lớp không được để trống.");

            _lopDAO.Delete(maLop);
        }
    }
}
