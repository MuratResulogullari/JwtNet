using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtNet.Entities.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "str_name_required")]
        [MaxLength(50, ErrorMessage = "str_max50_char")]
        public string Name { get; set; }
        [Required(ErrorMessage = "str_surname_required")]
        [MaxLength(50, ErrorMessage = "str_max50_char")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "str_email_required")]
        [MaxLength(50, ErrorMessage = "str_max50_char")]
        public string Email { get; set; }
        [Required(ErrorMessage = "str_password_required")]
        [MaxLength(50, ErrorMessage = "str_max50_char")]
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}