using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtNet.Entities.DbModels
{
    [Table("RefreshTokens")]
    public class RefreshToken : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(Role))]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public int TokenType { get; set; }
    }
}
