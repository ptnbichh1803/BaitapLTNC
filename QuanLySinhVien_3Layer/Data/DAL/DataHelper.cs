using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace QuanlySinhVienApp.Data.DAL
{
    public class DataHelper
    {
        private MySqlConnection con;
        // Thay đổi thông tin chuỗi kết nối phù hợp với máy của bạn
        private string connectionString = "server=127.0.0.1;database=Quanlysinhvien_db;uid=root;pwd=Ngocbich18!;";

        public DataHelper()
        {
            con = new MySqlConnection(connectionString);
        }

        public DataHelper(string customConnectionString)
        {
            this.connectionString = customConnectionString;
            con = new MySqlConnection(this.connectionString);
        }

        public void OpenConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        // Dùng cho INSERT, UPDATE, DELETE
        public int ExecuteNonQuery(string sql)
        {
            int rowsAffected = 0;
            try
            {
                OpenConnection();
                using (MySqlCommand command = new MySqlCommand(sql, con))
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            finally
            {
                CloseConnection();
            }
            return rowsAffected;
        }

        // Dùng cho SELECT trả về bảng dữ liệu (DataTable)
        public DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                using (MySqlCommand command = new MySqlCommand(sql, con))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }
    }
}