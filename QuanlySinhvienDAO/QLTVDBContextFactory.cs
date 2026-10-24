using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace QuanLyThuVienDAO
{
    public class QLTVDBContextFactory : IDesignTimeDbContextFactory<QLTVDBContext>
    {
        public QLTVDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QLTVDBContext>();
            string connStr = "server=127.0.0.1;database=Quanlysinhvien_db;uid=root;pwd=Ngocbich18!;";
            optionsBuilder.UseMySQL(connStr);

            return new QLTVDBContext(optionsBuilder.Options);
        }
    }
}
