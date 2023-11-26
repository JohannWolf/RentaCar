using Microsoft.EntityFrameworkCore;
using RentaCar.Models;

namespace RentaCar.Data
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
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Rates> Rates { get; set; }
        public DbSet<RateTypes> RatesTypes { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
    }
}
