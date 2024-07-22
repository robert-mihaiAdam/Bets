using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.Dto

{
    public class PlacedBetsDto
    {
        [Required]
        public Abstraction.BetsOptions Type { get; set; }

        [Required]
        public Guid QuoteId { get; set; }
    }
}
