using System.ComponentModel.DataAnnotations;

namespace JwtNet.Entities
{
    public class BaseEntity
    {
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public DateTime CreatedOn { get; set; }= DateTime.Now;
    }
}
