using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVienDAO.Entity
{
    [Table("Books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [Column(TypeName = "varchar(150)")]
        public string Publisher { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int CopyCount { get; set; }

        [Required]
        [MaxLength(150)]
        [Column(TypeName = "varchar(150)")]
        public string Author { get; set; } = string.Empty;

        public int PublishYear { get; set; }

        // Khóa ngoại liên kết với Category
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        // Quan hệ 1 - N với BookCopy
        public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();
    }
}
