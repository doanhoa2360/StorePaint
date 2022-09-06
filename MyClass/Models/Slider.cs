using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Sliders")]
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "không để trống")]
        public string Name { get; set; }
        public string Url { get; set; }
        public string Img { get; set; }
        public int? Orders { get; set; }
        public string Position { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
