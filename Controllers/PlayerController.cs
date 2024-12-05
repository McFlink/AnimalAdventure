using AnimalAdventure.Data;
using AnimalAdventure.Data.Entities;
using AnimalAdventure.DTOs;
using AnimalAdventure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdventure.Controllers
{
    [ApiController]
    [Route("api/player")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly AppDbContext context;
        public PlayerController(IPlayerService playerService, AppDbContext context)
        {
            _playerService = playerService;
            this.context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var player = await _playerService.RegisterPlayerAsync(dto);
                if (player == null)
                {
                    return NotFound();
                }

                // Skapar ett svar med HTTP-statuskoden 201 (Created) och inkluderar en URL till GetPlayerById.
                // URL:en byggs med hjälp av id för den nyligen skapade spelaren (player.Id).
                // Svaret innehåller också själva spelaren som JSON-data, så att klienten direkt kan använda informationen om den nya spelaren.
                return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, player);

            }
            catch
            {
                Console.WriteLine("Error when processing register POST request");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            try
            {
                var player = await _playerService.GetPlayerByIdAsync(id);
                if (player == null)
                {
                    return NotFound();
                }

                return Ok(player);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Player Controller: {ex}");
                return StatusCode(500, "Error Processing GET request att api/player/id");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _playerService.LoginPlayerAsync(login);

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("Invalid username or pin code");
                }

                return Ok(new { token });
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("getplayers")]
        public async Task<IActionResult> GetPlayers()
        {
            var playerName = User.Identity.Name;

            if (string.IsNullOrEmpty(playerName))
            {
                return Unauthorized("Användarnamn kunde inte hämtas från token.");
            }

            // Gå igenom service osv senare..
            var players = await context.Players
                .ToListAsync();

            return Ok(players);
        }
    }
}
