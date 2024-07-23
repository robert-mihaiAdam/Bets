using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.BetableEntity
{
    public class PlaceBetableEntityDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
