using Microsoft.EntityFrameworkCore;
using QuanLyThuVienDAO.Entity;

namespace QuanLyThuVienDAO
{
    public class QLTVDBContext : DbContext
    {
        public QLTVDBContext()
        {
        }

        public QLTVDBContext(DbContextOptions<QLTVDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookCopy> BookCopies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Chuỗi kết nối MySQL
                string connStr = "server=127.0.0.1;database=Quanlysinhvien_db;uid=root;pwd=Ngocbich18!;";
                optionsBuilder.UseMySQL(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BookCopy>(entity =>
            {
                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookCopies)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
