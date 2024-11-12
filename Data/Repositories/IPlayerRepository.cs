using AnimalAdventure.Data.Entities;
using AnimalAdventure.DTOs;

namespace AnimalAdventure.Data.Repositories
{
    public interface IPlayerRepository
    {
        Task RegisterPlayerAsync(Player player);
        Task<Player> GetPlayerById(int id);
        Task<Player> GetPlayerByNameAsync(LoginDTO login);
    }
}
