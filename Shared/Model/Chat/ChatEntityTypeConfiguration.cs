using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Model.Message;
using WheelOfFortune.Shared.Model.RealEstate;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Shared.Model.Chat
{
    internal class ChatEntityTypeConfiguration : IEntityTypeConfiguration<ChatEntity>
    {
        public void Configure(EntityTypeBuilder<ChatEntity> entity)
        {
            entity
                .HasMany(c => c.Users)
                .WithMany(u => u.Chats)
                .UsingEntity("ChatUsers");


        }
    }
}
