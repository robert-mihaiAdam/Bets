﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class BetQuotes
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [JsonIgnore]
        public Guid BetId { get; set; }

        [Range(1.00, 100.00)]
        public decimal QuoteA { get; set; } = 1.00m;

        [Range(1.00, 100.00)]
        public decimal QuoteB { get; set; } = 1.00m;

        [Range(1.00, 100.00)]
        public decimal QuoteX { get; set; } = 1.00m;
    }

    public class UpdateBetQuotes
    {
        public decimal? QuoteA { get; set; }

        public decimal? QuoteB { get; set; }

        public decimal? QuoteX { get; set; }
    }
}
