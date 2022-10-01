using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JwtNet.Entities.DbModels
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "str_name_required")]
        [MaxLength(50, ErrorMessage = "str_max50_char")]
        public string Name { get; set; }
        [Required(ErrorMessage = "str_surname_required")]
        [MaxLength(50, ErrorMessage = "str_max50_char")]
        public string Surname { get; set; }
        [Required]
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        [StringLength(50)]
        [Required]
        public string UserName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public RefreshToken RefreshToken { get; set; }

    }
}
