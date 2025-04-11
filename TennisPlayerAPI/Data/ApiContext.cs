using Microsoft.EntityFrameworkCore;
using TennisPlayerAPI.Models;

namespace TennisPlayerAPI.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<TennisPlayer> TennisPlayers {get; set;}
        public DbSet<Country> Countries { get; set; }

        public DbSet<PlayerData> PlayerDatas { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options)
            :base(options)
        {

        }
    }
}
