using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.BetableEntity
{
    public class UpdateBetableEntityDto
    {
        [StringLength(255)]
        public string? Name { get; set; }
    }
}
