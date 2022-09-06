using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public string Detail { get; set; }
        public string Img { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        public int Number { get; set; }
        public string MetaKey { get; set; }
        public string MetaDesc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
