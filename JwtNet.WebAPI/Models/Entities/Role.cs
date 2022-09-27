using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JwtNet.WebAPI.Models.Entities
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
