using Microsoft.EntityFrameworkCore;

namespace VideoGameApi.Data;

public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : DbContext(options)
{
    public DbSet<VideoGame> VideoGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<VideoGame>()
            .HasData(
                new VideoGame
                {
                    Id = 1,
                    Title = "The Legend of Zelda: Breath of the Wild",
                    Platform = "Nintendo Switch",
                    ReleaseDate = "2017-03-03",
                    Developer = "Nintendo EPD",
                    Publisher = "Nintendo",
                },
                new VideoGame
                {
                    Id = 2,
                    Title = "God of War",
                    Platform = "PlayStation 4",
                    ReleaseDate = "2018-04-20",
                    Developer = "Santa Monica Studio",
                    Publisher = "Sony Interactive Entertainment",
                },
                new VideoGame
                {
                    Id = 3,
                    Title = "Minecraft",
                    Platform = "Multi-platform",
                    ReleaseDate = "2011-11-18",
                    Developer = "Mojang Studios",
                    Publisher = "Mojang Studios",
                }
            );
    }
}
