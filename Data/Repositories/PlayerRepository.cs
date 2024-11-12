using AnimalAdventure.Data.Entities;
using AnimalAdventure.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace AnimalAdventure.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Player> GetPlayerById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid player id", nameof(id));
            }

            return await _context.Players.FindAsync(id);
        }

        public async Task<Player> GetPlayerByNameAsync(LoginDTO login)
        {
            try
            {
                var player = await _context.Players.FirstOrDefaultAsync(p => p.Name == login.Name && p.PinCode == login.PinCode);
                if (player == null)
                {
                    throw new InvalidOperationException();
                }

                return player;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while logging in", ex);
            }

            return null; // Null indicates no match found or incorrect pin code
        }

        public async Task RegisterPlayerAsync(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            _context.Players.Add(player);
            await _context.SaveChangesAsync();
        }
    }
}
