using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer.Data.DAL
{
    public class SinhVienDAL
    {
        private readonly string _connectionString;
        private static bool? _hasTenLopColumn = null;

        public SinhVienDAL()
        {
            string connStr = null;
            if (ConfigurationManager.ConnectionStrings["StrConn"] != null)
                connStr = ConfigurationManager.ConnectionStrings["StrConn"].ConnectionString;
            else if (ConfigurationManager.ConnectionStrings["MyConn"] != null)
                connStr = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;

            if (string.IsNullOrEmpty(connStr))
                connStr = "Server=localhost;Database=Quanlysinhvien_db;Uid=root;Pwd=Ngocbich18!;";

            _connectionString = connStr;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        private bool HasTenLopColumn(MySqlConnection conn)
        {
            if (_hasTenLopColumn.HasValue) return _hasTenLopColumn.Value;
            try
            {
                using (var cmd = new MySqlCommand("SHOW COLUMNS FROM SinhVien LIKE 'TenLop'", conn))
                {
                    var result = cmd.ExecuteScalar();
                    _hasTenLopColumn = (result != null);
                }
            }
            catch
            {
                _hasTenLopColumn = false;
            }
            return _hasTenLopColumn.Value;
        }

        public List<SinhVien> GetAll()
        {
            var list = new List<SinhVien>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, sv.DienThoai, sv.MaLop, 
                                        COALESCE(l.TenLop, '') AS TenLop, sv.Diem, sv.TrangThai 
                                 FROM SinhVien sv 
                                 LEFT JOIN Lop l ON sv.MaLop = l.MaLop 
                                 ORDER BY sv.MaSV";

                using (var cmd = new MySqlCommand(query, conn))
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
            if (string.IsNullOrWhiteSpace(maSV)) return null;

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, sv.DienThoai, sv.MaLop, 
                                        COALESCE(l.TenLop, '') AS TenLop, sv.Diem, sv.TrangThai 
                                 FROM SinhVien sv 
                                 LEFT JOIN Lop l ON sv.MaLop = l.MaLop 
                                 WHERE sv.MaSV = @MaSV";

                using (var cmd = new MySqlCommand(query, conn))
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

        public bool Insert(SinhVien sv)
        {
            if (sv == null || string.IsNullOrWhiteSpace(sv.MaSV)) return false;

            using (var conn = GetConnection())
            {
                conn.Open();
                bool includeTenLop = HasTenLopColumn(conn);

                string query = includeTenLop
                    ? @"INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, DienThoai, MaLop, TenLop, Diem, TrangThai) 
                        VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @DienThoai, @MaLop, @TenLop, @Diem, @TrangThai)"
                    : @"INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, DienThoai, MaLop, Diem, TrangThai) 
                        VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @DienThoai, @MaLop, @Diem, @TrangThai)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", sv.MaSV.Trim());
                    cmd.Parameters.AddWithValue("@HoTen", sv.HoTen ?? string.Empty);
                    cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
                    cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Email", sv.Email ?? string.Empty);
                    cmd.Parameters.AddWithValue("@DienThoai", sv.DienThoai ?? string.Empty);
                    cmd.Parameters.AddWithValue("@MaLop", sv.MaLop ?? string.Empty);
                    if (includeTenLop)
                    {
                        cmd.Parameters.AddWithValue("@TenLop", sv.TenLop ?? string.Empty);
                    }
                    cmd.Parameters.AddWithValue("@Diem", sv.Diem);
                    cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai ?? "Đang học");

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(SinhVien sv)
        {
            if (sv == null || string.IsNullOrWhiteSpace(sv.MaSV)) return false;

            using (var conn = GetConnection())
            {
                conn.Open();
                bool includeTenLop = HasTenLopColumn(conn);

                string query = includeTenLop
                    ? @"UPDATE SinhVien 
                        SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, 
                            Email = @Email, DienThoai = @DienThoai, MaLop = @MaLop, 
                            TenLop = @TenLop, Diem = @Diem, TrangThai = @TrangThai 
                        WHERE MaSV = @MaSV"
                    : @"UPDATE SinhVien 
                        SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, 
                            Email = @Email, DienThoai = @DienThoai, MaLop = @MaLop, 
                            Diem = @Diem, TrangThai = @TrangThai 
                        WHERE MaSV = @MaSV";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", sv.MaSV.Trim());
                    cmd.Parameters.AddWithValue("@HoTen", sv.HoTen ?? string.Empty);
                    cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
                    cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Email", sv.Email ?? string.Empty);
                    cmd.Parameters.AddWithValue("@DienThoai", sv.DienThoai ?? string.Empty);
                    cmd.Parameters.AddWithValue("@MaLop", sv.MaLop ?? string.Empty);
                    if (includeTenLop)
                    {
                        cmd.Parameters.AddWithValue("@TenLop", sv.TenLop ?? string.Empty);
                    }
                    cmd.Parameters.AddWithValue("@Diem", sv.Diem);
                    cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai ?? "Đang học");

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return false;

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV.Trim());
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<SinhVien> Search(string keyword, string maLop, decimal? minScore)
        {
            var list = new List<SinhVien>();

            using (var conn = GetConnection())
            {
                conn.Open();

                string query = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, sv.DienThoai, sv.MaLop, 
                                        COALESCE(l.TenLop, '') AS TenLop, sv.Diem, sv.TrangThai 
                                 FROM SinhVien sv 
                                 LEFT JOIN Lop l ON sv.MaLop = l.MaLop 
                                 WHERE 1=1";

                using (var cmd = new MySqlCommand())
                {
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        query += " AND (sv.MaSV LIKE @kw OR sv.HoTen LIKE @kw OR sv.Email LIKE @kw OR sv.DienThoai LIKE @kw)";
                        cmd.Parameters.AddWithValue("@kw", "%" + keyword.Trim() + "%");
                    }

                    if (!string.IsNullOrWhiteSpace(maLop) &&
                        !maLop.Equals("ALL", StringComparison.OrdinalIgnoreCase) &&
                        !maLop.Equals("-- Tất cả lớp --", StringComparison.OrdinalIgnoreCase))
                    {
                        query += " AND sv.MaLop = @maLop";
                        cmd.Parameters.AddWithValue("@maLop", maLop.Trim());
                    }

                    if (minScore.HasValue)
                    {
                        query += " AND sv.Diem >= @minScore";
                        cmd.Parameters.AddWithValue("@minScore", minScore.Value);
                    }

                    query += " ORDER BY sv.MaSV";
                    cmd.CommandText = query;
                    cmd.Connection = conn;

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

        private SinhVien MapSinhVien(MySqlDataReader reader)
        {
            return new SinhVien
            {
                MaSV = reader["MaSV"] != DBNull.Value ? reader["MaSV"].ToString() : string.Empty,
                HoTen = reader["HoTen"] != DBNull.Value ? reader["HoTen"].ToString() : string.Empty,
                NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : DateTime.MinValue,
                GioiTinh = reader["GioiTinh"] != DBNull.Value ? reader["GioiTinh"].ToString() : string.Empty,
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                DienThoai = reader["DienThoai"] != DBNull.Value ? reader["DienThoai"].ToString() : string.Empty,
                MaLop = reader["MaLop"] != DBNull.Value ? reader["MaLop"].ToString() : string.Empty,
                TenLop = reader["TenLop"] != DBNull.Value ? reader["TenLop"].ToString() : string.Empty,
                Diem = reader["Diem"] != DBNull.Value ? Convert.ToDecimal(reader["Diem"]) : 0m,
                TrangThai = reader["TrangThai"] != DBNull.Value ? reader["TrangThai"].ToString() : string.Empty
            };
        }
    }
}
