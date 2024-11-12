using AnimalAdventure.Data.Entities;
using AnimalAdventure.DTOs;

namespace AnimalAdventure.Services
{
    public interface IPlayerService
    {
        Task<PlayerDTO> RegisterPlayerAsync(RegisterDTO dto);
        Task<PlayerDTO> GetPlayerByIdAsync(int id);
        Task<string> LoginPlayerAsync(LoginDTO login);
    }
}
