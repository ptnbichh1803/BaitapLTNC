using System;
using System.Collections.Generic;
using System.Linq;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer.Data.DAL
{
    public class SinhVienDAL
    {
        private static List<SinhVien> _danhSachSinhVien;

        static SinhVienDAL()
        {
            _danhSachSinhVien = new List<SinhVien>
            {
                new SinhVien("SV001", "Nguyễn Văn An", new DateTime(2003, 5, 15), "Nam", "an.nv@gmail.com", "0912345678", "CNTT1", "Công nghệ thông tin 1", 8.5m, "Đang học"),
                new SinhVien("SV002", "Trần Thị Mai", new DateTime(2003, 8, 20), "Nữ", "mai.tt@gmail.com", "0987654321", "CNTT1", "Công nghệ thông tin 1", 9.0m, "Đang học"),
                new SinhVien("SV003", "Lê Hoàng Long", new DateTime(2002, 11, 2), "Nam", "long.lh@gmail.com", "0901234567", "KTPM1", "Kỹ thuật phần mềm 1", 7.2m, "Đang học"),
                new SinhVien("SV004", "Phạm Thị Hương", new DateTime(2003, 3, 10), "Nữ", "huong.pt@gmail.com", "0934567890", "HTTT1", "Hệ thống thông tin 1", 6.8m, "Bảo lưu"),
                new SinhVien("SV005", "Vũ Minh Đức", new DateTime(2001, 1, 25), "Nam", "duc.vm@gmail.com", "0978901234", "KHMT1", "Khoa học máy tính 1", 8.9m, "Đã tốt nghiệp")
            };
        }

        public List<SinhVien> GetAll()
        {
            return new List<SinhVien>(_danhSachSinhVien);
        }

        public SinhVien GetById(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return null;
            return _danhSachSinhVien.FirstOrDefault(s => s.MaSV.Trim().Equals(maSV.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public bool Insert(SinhVien sv)
        {
            if (sv == null) return false;
            if (GetById(sv.MaSV) != null) return false;

            _danhSachSinhVien.Add(sv);
            return true;
        }

        public bool Update(SinhVien sv)
        {
            if (sv == null) return false;
            var existing = GetById(sv.MaSV);
            if (existing == null) return false;

            existing.HoTen = sv.HoTen;
            existing.NgaySinh = sv.NgaySinh;
            existing.GioiTinh = sv.GioiTinh;
            existing.Email = sv.Email;
            existing.DienThoai = sv.DienThoai;
            existing.MaLop = sv.MaLop;
            existing.TenLop = sv.TenLop;
            existing.Diem = sv.Diem;
            existing.TrangThai = sv.TrangThai;

            return true;
        }

        public bool Delete(string maSV)
        {
            var sv = GetById(maSV);
            if (sv == null) return false;

            return _danhSachSinhVien.Remove(sv);
        }

        public List<SinhVien> Search(string keyword, string maLop, decimal? minScore)
        {
            var query = _danhSachSinhVien.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim().ToLower();
                query = query.Where(s => 
                    (s.MaSV != null && s.MaSV.ToLower().Contains(kw)) ||
                    (s.HoTen != null && s.HoTen.ToLower().Contains(kw)) ||
                    (s.Email != null && s.Email.ToLower().Contains(kw)) ||
                    (s.DienThoai != null && s.DienThoai.Contains(kw))
                );
            }

            if (!string.IsNullOrWhiteSpace(maLop) && !maLop.Equals("ALL", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(s => s.MaLop != null && s.MaLop.Equals(maLop, StringComparison.OrdinalIgnoreCase));
            }

            if (minScore.HasValue)
            {
                query = query.Where(s => s.Diem >= minScore.Value);
            }

            return query.ToList();
        }
    }
}
