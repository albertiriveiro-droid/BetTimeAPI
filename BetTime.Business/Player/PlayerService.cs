using BetTime.Models;
using BetTime.Data;
using System.Collections.Generic;

namespace BetTime.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Player CreatePlayer(PlayerCreateDTO playerDto)
        {
            var player = new Player(playerDto.Name, playerDto.TeamId, playerDto.ShirtNumber, playerDto.Position);
            _playerRepository.AddPlayer(player);
            return player;
        }

        public Player? GetPlayerById(int id) => _playerRepository.GetPlayerById(id);

        public IEnumerable<Player> GetPlayersByTeam(int teamId) => _playerRepository.GetPlayerByTeam(teamId);

        public Player UpdatePlayer(int id, PlayerUpdateDTO playerDto)
        {
            var player = _playerRepository.GetPlayerById(id);
            if (player == null) return null;

            if (playerDto.Name != null) player.Name = playerDto.Name;
            if (playerDto.TeamId.HasValue) player.TeamId = playerDto.TeamId.Value;
            if (playerDto.ShirtNumber.HasValue) player.ShirtNumber = playerDto.ShirtNumber.Value;
            if (playerDto.Position.HasValue) player.Position = playerDto.Position.Value;
            if (playerDto.IsActive.HasValue) player.IsActive = playerDto.IsActive.Value;

            _playerRepository.UpdatePlayer(player);
            return player;
        }

        public bool DeletePlayer(int id)
        {
            var player = _playerRepository.GetPlayerById(id);
            if (player == null) return false;

            _playerRepository.DeletePlayer(player);
            return true;
        }
    }
}