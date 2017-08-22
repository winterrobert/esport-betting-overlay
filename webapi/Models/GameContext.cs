using Microsoft.EntityFrameworkCore;

namespace esport_betting_overlay.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }

    }
}