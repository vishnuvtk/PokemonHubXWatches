using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<HeldItem> HeldItems { get; set; }
        public DbSet<Build> Builds { get; set; }

        public new DbSet<User> Users { get; set; }
        public DbSet<Watch> Watches { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Watch)
                .WithOne(w => w.Reservation)
                .HasForeignKey<Reservation>(r => r.WatchID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);
        }

    }
}
