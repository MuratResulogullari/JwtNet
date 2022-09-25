using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JwtNet.WebAPI.Models.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        [StringLength(50)]
        [Required]
        public string UserName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

    }
}
