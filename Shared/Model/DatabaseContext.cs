﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WheelOfFortune.Shared.Model.Chat;
using WheelOfFortune.Shared.Model.Message;
using WheelOfFortune.Shared.Model.RealEstate;
using WheelOfFortune.Shared.Model.Record;
using WheelOfFortune.Shared.Model.Rent;
using WheelOfFortune.Shared.Model.Tokens;
using WheelOfFortune.Shared.Model.User;
namespace WheelOfFortune.Shared.Model
{
    public class DatabaseContext : DbContext
    {

        public DbSet<RentEntity> Rents { get; set; } 
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<RealEstateEntity> RealEstates { get; set; }
        public DbSet<RecordEntity> Records { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
