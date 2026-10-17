using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer.Data.DAL
{
    /// <summary>
    /// Data Access Object cho SinhVien sử dụng ADO.NET (MySqlConnection).
    /// Tất cả các thao tác với CSDL đều ném ngoại lệ khi có lỗi để tầng trên xử lý.
    /// </summary>
    public class SinhVienDAO
    {
        private readonly string _connectionString;

        public SinhVienDAO()
        {
            string connStr = null;
            if (ConfigurationManager.ConnectionStrings["StrConn"] != null)
            {
                connStr = ConfigurationManager.ConnectionStrings["StrConn"].ConnectionString;
            }
            else if (ConfigurationManager.ConnectionStrings["MyConn"] != null)
            {
                connStr = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
            }

            if (string.IsNullOrEmpty(connStr))
            {
                connStr = "Server=localhost;Database=Quanlysinhvien_db;Uid=root;Pwd=Ngocbich18!;";
            }

            _connectionString = connStr;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        private SinhVien MapSinhVien(IDataRecord reader)
        {
            DateTime ngaySinh = DateTime.MinValue;
            if (reader["NgaySinh"] != DBNull.Value)
            {
                DateTime.TryParse(reader["NgaySinh"].ToString(), out ngaySinh);
            }

            decimal diem = 0;
            if (reader["Diem"] != DBNull.Value)
            {
                decimal.TryParse(reader["Diem"].ToString(), out diem);
            }

            return new SinhVien
            {
                MaSV = reader["MaSV"] != DBNull.Value ? reader["MaSV"].ToString() : string.Empty,
                HoTen = reader["HoTen"] != DBNull.Value ? reader["HoTen"].ToString() : string.Empty,
                NgaySinh = ngaySinh,
                GioiTinh = reader["GioiTinh"] != DBNull.Value ? reader["GioiTinh"].ToString() : string.Empty,
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                DienThoai = reader["DienThoai"] != DBNull.Value ? reader["DienThoai"].ToString() : string.Empty,
                MaLop = reader["MaLop"] != DBNull.Value ? reader["MaLop"].ToString() : string.Empty,
                TenLop = reader["TenLop"] != DBNull.Value ? reader["TenLop"].ToString() : string.Empty,
                Diem = diem,
                TrangThai = reader["TrangThai"] != DBNull.Value ? reader["TrangThai"].ToString() : string.Empty
            };
        }

        public List<SinhVien> GetAll()
        {
            var list = new List<SinhVien>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, sv.DienThoai, sv.MaLop, 
                                      COALESCE(l.TenLop, sv.TenLop, '') AS TenLop, sv.Diem, sv.TrangThai 
                               FROM SinhVien sv 
                               LEFT JOIN Lop l ON sv.MaLop = l.MaLop 
                               ORDER BY sv.MaSV";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapSinhVien(reader));
                    }
                }
            }
            return list;
        }

        public SinhVien GetById(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new ArgumentException("Mã sinh viên không được để trống.", "maSV");

            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, sv.DienThoai, sv.MaLop, 
                                      COALESCE(l.TenLop, sv.TenLop, '') AS TenLop, sv.Diem, sv.TrangThai 
                               FROM SinhVien sv 
                               LEFT JOIN Lop l ON sv.MaLop = l.MaLop 
                               WHERE sv.MaSV = @MaSV";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV.Trim());
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapSinhVien(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<SinhVien> GetByMaLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new ArgumentException("Mã lớp không được để trống.", "maLop");

            var list = new List<SinhVien>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, sv.DienThoai, sv.MaLop, 
                                      COALESCE(l.TenLop, sv.TenLop, '') AS TenLop, sv.Diem, sv.TrangThai 
                               FROM SinhVien sv 
                               LEFT JOIN Lop l ON sv.MaLop = l.MaLop 
                               WHERE sv.MaLop = @MaLop 
                               ORDER BY sv.MaSV";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop.Trim());
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapSinhVien(reader));
                        }
                    }
                }
            }
            return list;
        }

        public void Add(SinhVien sv)
        {
            if (sv == null)
                throw new ArgumentNullException("sv", "Dữ liệu sinh viên không được để trống.");

            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, DienThoai, MaLop, TenLop, Diem, TrangThai) 
                               VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @DienThoai, @MaLop, @TenLop, @Diem, @TrangThai)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", sv.MaSV.Trim());
                    cmd.Parameters.AddWithValue("@HoTen", sv.HoTen ?? string.Empty);
                    cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
                    cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Email", sv.Email ?? string.Empty);
                    cmd.Parameters.AddWithValue("@DienThoai", sv.DienThoai ?? string.Empty);
                    cmd.Parameters.AddWithValue("@MaLop", sv.MaLop ?? string.Empty);
                    cmd.Parameters.AddWithValue("@TenLop", sv.TenLop ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Diem", sv.Diem);
                    cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai ?? "Đang học");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(SinhVien sv)
        {
            if (sv == null)
                throw new ArgumentNullException("sv", "Dữ liệu sinh viên không được để trống.");

            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE SinhVien 
                               SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, 
                                   Email = @Email, DienThoai = @DienThoai, MaLop = @MaLop, 
                                   TenLop = @TenLop, Diem = @Diem, TrangThai = @TrangThai 
                               WHERE MaSV = @MaSV";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", sv.MaSV.Trim());
                    cmd.Parameters.AddWithValue("@HoTen", sv.HoTen ?? string.Empty);
                    cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
                    cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Email", sv.Email ?? string.Empty);
                    cmd.Parameters.AddWithValue("@DienThoai", sv.DienThoai ?? string.Empty);
                    cmd.Parameters.AddWithValue("@MaLop", sv.MaLop ?? string.Empty);
                    cmd.Parameters.AddWithValue("@TenLop", sv.TenLop ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Diem", sv.Diem);
                    cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai ?? "Đang học");

                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new Exception("Không tìm thấy sinh viên có mã '" + sv.MaSV + "' để cập nhật.");
                    }
                }
            }
        }

        public void Delete(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new ArgumentException("Mã sinh viên không được để trống.", "maSV");

            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV.Trim());
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new Exception("Không tìm thấy sinh viên có mã '" + maSV + "' để xoá.");
                    }
                }
            }
        }

        public List<SinhVien> Search(string keyword, string maLop, decimal? minScore)
        {
            var list = new List<SinhVien>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, sv.DienThoai, sv.MaLop, 
                                      COALESCE(l.TenLop, sv.TenLop, '') AS TenLop, sv.Diem, sv.TrangThai 
                               FROM SinhVien sv 
                               LEFT JOIN Lop l ON sv.MaLop = l.MaLop 
                               WHERE 1=1 ";

                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    sql += " AND (sv.MaSV LIKE @Keyword OR sv.HoTen LIKE @Keyword OR sv.Email LIKE @Keyword OR sv.DienThoai LIKE @Keyword) ";
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(maLop) && maLop != "ALL")
                {
                    sql += " AND sv.MaLop = @MaLop ";
                    cmd.Parameters.AddWithValue("@MaLop", maLop.Trim());
                }

                if (minScore.HasValue)
                {
                    sql += " AND sv.Diem >= @MinScore ";
                    cmd.Parameters.AddWithValue("@MinScore", minScore.Value);
                }

                sql += " ORDER BY sv.MaSV";
                cmd.CommandText = sql;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapSinhVien(reader));
                    }
                }
            }
            return list;
        }
    }
}
