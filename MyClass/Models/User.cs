using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "UserName không được để trống!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password không được để trống!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "FullName không được để trống!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email không được để trống!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone không được để trống!")]
        public string Phone { get; set; }

        public string Img { get; set; }

        public int? CountError { get; set; }
        //[Required(ErrorMessage = "Roles không được để trống!")]
        public string Roles { get; set; }
        public int? Gender { get; set; }
        public string Address { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
