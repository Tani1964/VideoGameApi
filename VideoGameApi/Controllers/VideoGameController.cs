using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;

namespace VideoGameApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoGameController(VideoGameDbContext context) : ControllerBase
{
    private readonly VideoGameDbContext _context = context;

    // private static List<VideoGame> videoGames = new List<VideoGame>
    // {
    //     new VideoGame
    //     {
    //         Id = 1,
    //         Title = "The Legend of Zelda: Breath of the Wild",
    //         Platform = "Nintendo Switch",
    //         ReleaseDate = "2017-03-03",
    //         Developer = "Nintendo EPD",
    //         Publisher = "Nintendo",
    //     },
    //     new VideoGame
    //     {
    //         Id = 2,
    //         Title = "God of War",
    //         Platform = "PlayStation 4",
    //         ReleaseDate = "2018-04-20",
    //         Developer = "Santa Monica Studio",
    //         Publisher = "Sony Interactive Entertainment",
    //     },
    //     new VideoGame
    //     {
    //         Id = 3,
    //         Title = "Minecraft",
    //         Platform = "Multi-platform",
    //         ReleaseDate = "2011-11-18",
    //         Developer = "Mojang Studios",
    //         Publisher = "Mojang Studios",
    //     },
    // };

    [HttpGet]
    public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
    {
        return Ok(await _context.VideoGames.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VideoGame>> GetVideoGameById(int id)
    {
        var videoGame = await _context.VideoGames.FindAsync(id);
        if (videoGame == null)
        {
            return NotFound();
        }
        return Ok(videoGame);
    }

    [HttpPost]
    public async Task<ActionResult<VideoGame>> CreateVideoGame(VideoGame videoGame)
    {
        if (videoGame == null || string.IsNullOrEmpty(videoGame.Title))
        {
            return BadRequest("Invalid video game data.");
        }
        _context.VideoGames.Add(videoGame);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetVideoGameById), new { id = videoGame.Id }, videoGame);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVideoGame(int id, VideoGame updatedVideoGame)
    {
        var videoGame = await _context.VideoGames.FindAsync(id);
        if (videoGame == null)
        {
            return NotFound();
        }

        if (updatedVideoGame == null || string.IsNullOrEmpty(updatedVideoGame.Title))
        {
            return BadRequest("Invalid video game data.");
        }

        videoGame.Title = updatedVideoGame.Title;
        videoGame.Platform = updatedVideoGame.Platform;
        videoGame.ReleaseDate = updatedVideoGame.ReleaseDate;
        videoGame.Developer = updatedVideoGame.Developer;
        videoGame.Publisher = updatedVideoGame.Publisher;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideoGame(int id)
    {
        var videoGame =  await _context.VideoGames.FindAsync(id);
        if (videoGame == null)
        {
            return NotFound();
        }
        _context.VideoGames.Remove(videoGame);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
