using System.ComponentModel.DataAnnotations;

namespace JwtNet.WebAPI.Models.Entities
{
    public class RefreshToken : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public int TokenType { get; set; }

        
    }
}
