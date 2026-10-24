using Microsoft.EntityFrameworkCore;
using QuanlyThuVienDAO.Entity;

namespace QuanlyThuVienDAO
{
    public class QLTVDBContext : DbContext
    {
        public QLTVDBContext()
        {
        }

        public QLTVDBContext(DbContextOptions<QLTVDBContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Thay thế chuỗi kết nối (Connection String) phù hợp với SQL Server của bạn
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=QuanLyThuVienDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}