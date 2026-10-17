using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using QuanLySinhVien_3Layer.Data.DAL;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer.BUL
{
    public class SinhVienBUL
    {
        private readonly SinhVienDAO _sinhVienDAO;

        public SinhVienBUL()
        {
            _sinhVienDAO = new SinhVienDAO();
        }

        public List<SinhVien> GetAll()
        {
            return _sinhVienDAO.GetAll();
        }

        public SinhVien GetById(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new ArgumentException("Mã sinh viên không được để trống.", "maSV");

            return _sinhVienDAO.GetById(maSV.Trim());
        }

        public void KiemTraThongTin(SinhVien sv, bool isThemMoi)
        {
            if (sv == null)
            {
                throw new ArgumentNullException("sv", "Dữ liệu sinh viên không hợp lệ!");
            }

            if (string.IsNullOrWhiteSpace(sv.MaSV))
            {
                throw new ArgumentException("Mã sinh viên không được để trống!");
            }

            sv.MaSV = sv.MaSV.Trim();

            if (isThemMoi)
            {
                if (_sinhVienDAO.GetById(sv.MaSV) != null)
                {
                    throw new InvalidOperationException("Mã sinh viên '" + sv.MaSV + "' đã tồn tại trong hệ thống!");
                }
            }
            else
            {
                if (_sinhVienDAO.GetById(sv.MaSV) == null)
                {
                    throw new InvalidOperationException("Không tìm thấy sinh viên có mã '" + sv.MaSV + "' để cập nhật!");
                }
            }

            if (string.IsNullOrWhiteSpace(sv.HoTen))
            {
                throw new ArgumentException("Họ và tên không được để trống!");
            }

            sv.HoTen = sv.HoTen.Trim();

            if (sv.NgaySinh > DateTime.Now)
            {
                throw new ArgumentException("Ngày sinh không được lớn hơn ngày hiện tại!");
            }

            if (string.IsNullOrWhiteSpace(sv.MaLop))
            {
                throw new ArgumentException("Vui lòng chọn lớp học!");
            }

            if (sv.Diem < 0 || sv.Diem > 10)
            {
                throw new ArgumentException("Điểm phải nằm trong thang điểm từ 0 đến 10!");
            }

            if (!string.IsNullOrWhiteSpace(sv.Email))
            {
                sv.Email = sv.Email.Trim();
                if (!Regex.IsMatch(sv.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    throw new ArgumentException("Định dạng email không hợp lệ (ví dụ: an.nv@gmail.com)!");
                }
            }

            if (!string.IsNullOrWhiteSpace(sv.DienThoai))
            {
                sv.DienThoai = sv.DienThoai.Trim();
                if (!Regex.IsMatch(sv.DienThoai, @"^0\d{9,10}$"))
                {
                    throw new ArgumentException("Số điện thoại không hợp lệ (phải bắt đầu bằng 0 và có từ 10 chữ số)!");
                }
            }
        }

        public void Them(SinhVien sv)
        {
            KiemTraThongTin(sv, true);
            _sinhVienDAO.Add(sv);
        }

        public void Sua(SinhVien sv)
        {
            KiemTraThongTin(sv, false);
            _sinhVienDAO.Update(sv);
        }

        public void Xoa(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
            {
                throw new ArgumentException("Vui lòng chọn sinh viên cần xoá!");
            }

            string cleanMaSV = maSV.Trim();
            if (_sinhVienDAO.GetById(cleanMaSV) == null)
            {
                throw new InvalidOperationException("Không tìm thấy sinh viên có mã '" + cleanMaSV + "' để xoá!");
            }

            _sinhVienDAO.Delete(cleanMaSV);
        }

        public List<SinhVien> TimKiem(string keyword, string maLop, decimal? minScore)
        {
            return _sinhVienDAO.Search(keyword, maLop, minScore);
        }
    }
}
