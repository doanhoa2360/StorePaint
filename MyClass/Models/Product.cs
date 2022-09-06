using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int CatId { get; set; }
        public int SupplierId { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không để rỗng!")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Chi tiết sản phẩm không để rỗng!")]
        public string Detail { get; set; }
        public string Img { get; set; }
        [Required(ErrorMessage = "Giá gốc sản phẩm không để rỗng!")]
        public decimal Price  {get; set; }
        [Required(ErrorMessage = "Giá bán sản phẩm không để rỗng!")]
        public decimal PriceSale { get; set; }
        public int Number { get; set; }
        [Required(ErrorMessage = "Khóa tìm kiếm không để rỗng!")]
        public string MetaKey { get; set; }
        [Required(ErrorMessage = "Khóa mô tả không để rỗng!")]
        public string MetaDesc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
