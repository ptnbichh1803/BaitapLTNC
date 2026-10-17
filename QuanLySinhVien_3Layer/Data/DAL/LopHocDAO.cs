using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer.Data.DAL
{
    /// <summary>
    /// Data Access Object cho LopHoc sử dụng ADO.NET (MySqlConnection).
    /// Tất cả các phương thức đều ném về ngoại lệ khi gặp lỗi.
    /// </summary>
    public class LopHocDAO
    {
        private readonly string _connectionString;

        public LopHocDAO()
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

        public List<Lop> GetAll()
        {
            var list = new List<Lop>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "SELECT MaLop, TenLop FROM Lop ORDER BY MaLop";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Lop
                        {
                            MaLop = reader["MaLop"] != DBNull.Value ? reader["MaLop"].ToString() : string.Empty,
                            TenLop = reader["TenLop"] != DBNull.Value ? reader["TenLop"].ToString() : string.Empty
                        });
                    }
                }
            }
            return list;
        }

        public Lop GetById(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new ArgumentException("Mã lớp không được để trống.", "maLop");

            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "SELECT MaLop, TenLop FROM Lop WHERE MaLop = @MaLop";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop.Trim());
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Lop
                            {
                                MaLop = reader["MaLop"] != DBNull.Value ? reader["MaLop"].ToString() : string.Empty,
                                TenLop = reader["TenLop"] != DBNull.Value ? reader["TenLop"].ToString() : string.Empty
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Lop lop)
        {
            if (lop == null)
                throw new ArgumentNullException("lop", "Dữ liệu lớp học không được để trống.");

            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Lop (MaLop, TenLop) VALUES (@MaLop, @TenLop)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", lop.MaLop != null ? lop.MaLop.Trim() : string.Empty);
                    cmd.Parameters.AddWithValue("@TenLop", lop.TenLop != null ? lop.TenLop.Trim() : string.Empty);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Lop lop)
        {
            if (lop == null)
                throw new ArgumentNullException("lop", "Dữ liệu lớp học không được để trống.");

            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "UPDATE Lop SET TenLop = @TenLop WHERE MaLop = @MaLop";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", lop.MaLop != null ? lop.MaLop.Trim() : string.Empty);
                    cmd.Parameters.AddWithValue("@TenLop", lop.TenLop != null ? lop.TenLop.Trim() : string.Empty);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new ArgumentException("Mã lớp không được để trống.", "maLop");

            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM Lop WHERE MaLop = @MaLop";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop.Trim());
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
