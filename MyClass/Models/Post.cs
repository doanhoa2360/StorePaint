using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn chủ đề!")]
        public int TopicId { get; set; }
        [Required(ErrorMessage = "Tiêu đề bài viết không để rỗng!")]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Nội dung bài viết không để rỗng!")]
        public string Detail { get; set; }
        public string Img { get; set; }
        public string PostType { get; set; }
        [Required(ErrorMessage = "Từ khóa SEO không để rỗng!")]
        public string Metakey { get; set; }
        [Required(ErrorMessage = "mô tả SEO không để rỗng!")]
        public string MetaDesc { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
