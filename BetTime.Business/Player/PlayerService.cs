using BetTime.Models;
using BetTime.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BetTime.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _repository;
        private readonly ITeamRepository _teamRepository;

        public PlayerService(IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _repository = playerRepository;
            _teamRepository=teamRepository;
        }

        public Player CreatePlayer(PlayerCreateDTO dto)
{
   
    if (string.IsNullOrWhiteSpace(dto.Name))
        throw new ArgumentException("Player name is required.");

  
    var team = _teamRepository.GetTeamById(dto.TeamId);
    if (team == null)
        throw new KeyNotFoundException($"Team with ID {dto.TeamId} not found.");

   
    if (_repository.PlayerNameExists(dto.Name))
        throw new InvalidOperationException("A player with this name already exists.");

   
    var player = new Player(
        dto.Name.Trim(),
        dto.TeamId,
        dto.ShirtNumber,
        dto.Position
    );

    try
    {
        _repository.AddPlayer(player);
    }
    catch (DbUpdateException)
    {
        throw new InvalidOperationException("A player with this name already exists.");
    }

    return player;
}

        public Player? GetPlayerById(int id) => _repository.GetPlayerById(id);

        public IEnumerable<Player> GetPlayersByTeam(int teamId) => _repository.GetPlayerByTeam(teamId);

        public Player UpdatePlayer(int playerId, PlayerUpdateDTO dto){
          
        var player = _repository.GetPlayerById(playerId)
        ?? throw new KeyNotFoundException($"Player with ID {playerId} not found.");

        if (!string.IsNullOrWhiteSpace(dto.Name) && dto.Name != player.Name)
        {

        if (_repository.PlayerNameExists(dto.Name))
            throw new InvalidOperationException("A player with this name already exists.");

        player.Name = dto.Name.Trim();
        }

 
        if (dto.TeamId.HasValue && dto.TeamId.Value != player.TeamId)
        {
        var team = _teamRepository.GetTeamById(dto.TeamId.Value);
        if (team == null)
            throw new KeyNotFoundException($"Team with ID {dto.TeamId.Value} not found.");

        player.TeamId = dto.TeamId.Value;
        }

  
        if (dto.ShirtNumber.HasValue) player.ShirtNumber = dto.ShirtNumber.Value;
        if (dto.Position.HasValue) player.Position = dto.Position.Value;
        if (dto.IsActive.HasValue) player.IsActive = dto.IsActive.Value;

   
         try
        {
        _repository.UpdatePlayer(player);
        }
        catch (DbUpdateException)
        {
        throw new InvalidOperationException("A player with this name already exists.");
        }

        return player;
    }

        public bool DeletePlayer(int id)
        {
            var player = _repository.GetPlayerById(id);
            if (player == null) return false;

            _repository.DeletePlayer(player);
            return true;
        }
    }
}