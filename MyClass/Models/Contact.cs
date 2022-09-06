using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [Required(ErrorMessage = "Tên liên hệ không để rỗng!")]
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Tiêu đề liên hệ không để rỗng!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Chi tiết liên hệ không để rỗng!")]
        public string Detail { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
