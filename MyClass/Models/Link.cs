using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Links")]
    public class Link
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public int TableId { get; set; }
        [Required]
        public string Type{ get; set; }


    }
}
