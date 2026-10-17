using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer.Data.DAL
{
    public class LopDAL
    {
        private readonly string _connectionString;

        public LopDAL()
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

        public List<Lop> GetAll()
        {
            var list = new List<Lop>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT MaLop, TenLop FROM Lop ORDER BY MaLop";
                using (var cmd = new MySqlCommand(query, conn))
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
            if (string.IsNullOrWhiteSpace(maLop)) return null;

            using (var conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT MaLop, TenLop FROM Lop WHERE MaLop = @MaLop";
                using (var cmd = new MySqlCommand(query, conn))
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
    }
}
