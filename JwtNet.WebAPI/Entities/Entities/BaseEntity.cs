using System.ComponentModel.DataAnnotations;

namespace JwtNet.WebAPI.Models.Entities
{
    public class BaseEntity
    {
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
