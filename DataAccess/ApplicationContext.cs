using DatabaseAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Wagon> Wagons { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<InfoUser> InfoUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=courseprojectdb;Trusted_Connection=True;", b => b.MigrationsAssembly("WebApp"));
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(x => x.Passengers)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Passenger>()
                .HasMany(x => x.Tickets)
                .WithOne(x => x.Passenger)
                .HasForeignKey(x => x.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Train>()
                .HasMany(x => x.Wagons)
                .WithOne(x => x.Train)
                .HasForeignKey(x => x.TrainId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Train>()
                .HasMany(x => x.Tickets)
                .WithOne(x => x.Train)
                .HasForeignKey(x => x.TrainId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Wagon>()
                .HasMany(x => x.Places)
                .WithOne(x => x.Wagon)
                .HasForeignKey(x => x.WagonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Train>()
                .HasMany(x => x.Stops)
                .WithOne(x => x.Train)
                .HasForeignKey(x => x.TrainId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Station>()
                .HasMany(x => x.Stops)
                .WithOne(x => x.Station)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InfoUser>()
                .HasOne(x => x.Passenger)
                .WithOne(x => x.InfoUser)
                .HasForeignKey<Passenger>(x => x.InfoUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<InfoUser>()
                .HasOne(x => x.User)
                .WithOne(x => x.InfoUser)
                .HasForeignKey<User>(x => x.InfoUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Place>()
                .HasOne(x => x.Ticket)
                .WithOne(x => x.Place)
                .HasForeignKey<Ticket>(x => x.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}