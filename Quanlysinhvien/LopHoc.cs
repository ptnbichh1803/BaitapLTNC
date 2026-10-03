using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quanlysinhvien
{
    public class LopHoc
    {
        [Required]
        [RegularExpression("^CSE[0-9]{4}$", ErrorMessage = "Mã lớp phải có định dạng CSExxxx, trong đó xxxx là 4 chữ số.")]
        public string MaLop { get; set; }
        [Required]
        [Range(5, 30, ErrorMessage = "Số lượng sinh viên phải từ 5 đến 30.")]
        public string TenLop { get; set; }
        public List<ValidationResult> IsInvalid()
        {
            var valResults = new List<ValidationResult>();
            var valContext = new ValidationContext(this);
            Validator.TryValidateObject(this, valContext, valResults, true);
            if(!kt)
            return valResults;
        }
    }
}
