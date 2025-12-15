using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BetTime.Models;

    public class PlayerMarket
    {
        public int Id { get; set; }
        [ForeignKey("Match")]
        public int MatchId { get; set; }
          [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public PlayerMarketType PlayerMarketType { get; set; }
       
        [JsonIgnore]
        public Player Player { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public Match Match { get; set; }
        public bool IsOpen { get; set; }
        public List<PlayerMarketSelection> Selections { get; set; } = new List<PlayerMarketSelection>();

        
        public PlayerMarket() { }

      
        public PlayerMarket(int matchId, int playerId, PlayerMarketType marketType)
        {
            MatchId = matchId;
            PlayerId = playerId;
            PlayerMarketType = marketType;
            CreatedAt = DateTime.UtcNow;
            IsOpen = true;
        }
    }
    