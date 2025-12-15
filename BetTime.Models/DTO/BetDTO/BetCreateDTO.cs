using System.ComponentModel.DataAnnotations;

namespace BetTime.Models;

    public class BetCreateDTO
    {
        [Required]
        public int UserId { get; set; }

        public int? MarketSelectionId { get; set; }

      
        public int? PlayerMarketSelectionId { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }