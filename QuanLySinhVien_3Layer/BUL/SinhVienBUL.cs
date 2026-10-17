using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using QuanLySinhVien_3Layer.Data.DAL;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer.BUL
{
    public class SinhVienBUL
    {
        private readonly SinhVienDAL _sinhVienDAL;

        public SinhVienBUL()
        {
            _sinhVienDAL = new SinhVienDAL();
        }

        public List<SinhVien> GetAll()
        {
            return _sinhVienDAL.GetAll();
        }

        public SinhVien GetById(string maSV)
        {
            return _sinhVienDAL.GetById(maSV);
        }

        public bool Them(SinhVien sv, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (sv == null)
            {
                errorMessage = "Dữ liệu sinh viên không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sv.MaSV))
            {
                errorMessage = "Mã sinh viên không được để trống!";
                return false;
            }

            if (_sinhVienDAL.GetById(sv.MaSV) != null)
            {
                errorMessage = $"Mã sinh viên '{sv.MaSV}' đã tồn tại trong hệ thống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sv.HoTen))
            {
                errorMessage = "Họ và tên không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sv.MaLop))
            {
                errorMessage = "Vui lòng chọn lớp học!";
                return false;
            }

            if (sv.Diem < 0 || sv.Diem > 10)
            {
                errorMessage = "Điểm phải nằm trong thang điểm từ 0 đến 10!";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(sv.Email) && !sv.Email.Contains("@"))
            {
                errorMessage = "Định dạng email không hợp lệ!";
                return false;
            }

            bool success = _sinhVienDAL.Insert(sv);
            if (!success)
            {
                errorMessage = "Không thể thêm sinh viên vào danh sách!";
                return false;
            }

            return true;
        }

        public bool Sua(SinhVien sv, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (sv == null)
            {
                errorMessage = "Dữ liệu sinh viên không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sv.MaSV))
            {
                errorMessage = "Mã sinh viên không được để trống!";
                return false;
            }

            if (_sinhVienDAL.GetById(sv.MaSV) == null)
            {
                errorMessage = $"Không tìm thấy sinh viên có mã '{sv.MaSV}' để cập nhật!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sv.HoTen))
            {
                errorMessage = "Họ và tên không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sv.MaLop))
            {
                errorMessage = "Vui lòng chọn lớp học!";
                return false;
            }

            if (sv.Diem < 0 || sv.Diem > 10)
            {
                errorMessage = "Điểm phải nằm trong thang điểm từ 0 đến 10!";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(sv.Email) && !sv.Email.Contains("@"))
            {
                errorMessage = "Định dạng email không hợp lệ!";
                return false;
            }

            bool success = _sinhVienDAL.Update(sv);
            if (!success)
            {
                errorMessage = "Không thể cập nhật thông tin sinh viên!";
                return false;
            }

            return true;
        }

        public bool Xoa(string maSV, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(maSV))
            {
                errorMessage = "Vui lòng chọn sinh viên cần xoá!";
                return false;
            }

            if (_sinhVienDAL.GetById(maSV) == null)
            {
                errorMessage = $"Không tìm thấy sinh viên có mã '{maSV}' để xoá!";
                return false;
            }

            bool success = _sinhVienDAL.Delete(maSV);
            if (!success)
            {
                errorMessage = "Không thể xoá sinh viên!";
                return false;
            }

            return true;
        }

        public List<SinhVien> TimKiem(string keyword, string maLop, decimal? minScore)
        {
            return _sinhVienDAL.Search(keyword, maLop, minScore);
        }
    }
}
