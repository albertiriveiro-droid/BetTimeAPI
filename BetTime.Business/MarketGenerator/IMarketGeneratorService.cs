using BetTime.Models;
using System.Collections.Generic;

namespace BetTime.Business;

public interface IMarketGeneratorService
{
    List<Market> GenerateMarketsForMatch(int matchId);
}