using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Topics")]
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name không để trống")]
        public String Name { get; set; }
        public String Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Orders { get; set; }
        [Required(ErrorMessage = "MetaKey không để trống")]

        public String MetaKey { get; set; }
        [Required(ErrorMessage = "MetaDesc không để trống")]

        public String MetaDesc { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
