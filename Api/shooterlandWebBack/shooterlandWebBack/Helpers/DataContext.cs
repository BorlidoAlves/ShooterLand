using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("dbShooterland"));
        }

        public DbSet<Entity.User> Users { get; set; }

        public DbSet<Entity.Achievement> Achievement { get; set; }

        public DbSet<Entity.StatsSingleplayer> StatsSingleplayer { get; set; }
        
        public DbSet<Entity.StatsMultiplayer> StatsMultiplayer { get; set; }
        
        public DbSet<Entity.UsersAchievements> UsersAchievements { get; set; }

    }
}
