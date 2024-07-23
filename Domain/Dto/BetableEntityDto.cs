using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class BetableEntityDto
    {

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
