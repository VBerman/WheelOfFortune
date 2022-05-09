using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Shared.Model.RealEstate;
using WheelOfFortune.Shared.Model.User;
namespace WheelOfFortune.Server
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RealEstateEntity> RealEstates { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

    }
}
