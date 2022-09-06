using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Categorys")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên loại sản phẩm không để rỗng!")]
        public string Name { get; set; }

        public string Slug { get; set; }
        public string Img { get; set; }
        public int? ParentId { get; set; }

        public int? Orders { get; set; }
        [Required(ErrorMessage = "Từ khóa SEO không để rỗng!")]
        public string MetaKey { get; set; }
        [Required(ErrorMessage = "Mô tả SEO không để rỗng!")]
        public string MetaDesc { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
