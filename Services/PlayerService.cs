using AnimalAdventure.Data.Entities;
using AnimalAdventure.Data.Repositories;
using AnimalAdventure.DTOs;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnimalAdventure.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IConfiguration config;
        public PlayerService(IPlayerRepository playerRepository, IConfiguration config)
        {
            _playerRepository = playerRepository;
            this.config = config;
        }

        public async Task<PlayerDTO> GetPlayerByIdAsync(int id)
        {
            try
            {
                var player = await _playerRepository.GetPlayerById(id);
                if (player == null)
                {
                    throw new InvalidOperationException($"No player found with id {id}");
                }

                return CreatePlayerDto(player);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error in GetPlayerByIdAsync");
                return null;
            }
        }

        public async Task<PlayerDTO> RegisterPlayerAsync(RegisterDTO dto)
        {
            // Create player
            var player = new Player
            {
                Name = dto.Name,
                PinCode = dto.PinCode
            };

            try
            {
                // Save player in db
                await _playerRepository.RegisterPlayerAsync(player);

                // Create dto and return to client
                var playerDTO = CreatePlayerDto(player);

                return playerDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering new player", ex);
                throw;
            }
        }

        public PlayerDTO CreatePlayerDto(Player player)
        {
            var playerDTO = new PlayerDTO()
            {
                Id = player.Id,
                Name = player.Name
            };

            return playerDTO;
        }

        public async Task<string> LoginPlayerAsync(LoginDTO login)
        {
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login), "Login data is required.");
            }

            var player = await _playerRepository.GetPlayerByNameAsync(login);

            if (player == null || player.PinCode != login.PinCode)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            // Create claims for token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, player.Name),
                new Claim(ClaimTypes.NameIdentifier, player.Id.ToString())
            };

            // Create signing key and credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:Key"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create token
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: "Jwt:Audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            // Return token as a string
            return await GenerateTokenAsync(player);
        }

        public async Task<string> GenerateTokenAsync(Player player)
        {
            try
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, player.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    config["Jwt:Issuer"],
                    config["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
