using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtNet.Entities.ViewModels
{
    public class RoleViewModel:BaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "str_rolename_required")]
        [MaxLength(50,ErrorMessage = "str_max50_char")]
        public string RoleName { get; set; }   
    }
}