using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Shared.Model.RealEstate
{
    internal class RealEstateEntityTypeConfiguration : IEntityTypeConfiguration<RealEstateEntity>
    {
        public void Configure(EntityTypeBuilder<RealEstateEntity> entity)
        {
            entity.HasOne(r => r.Landlord)
                .WithMany(u => u.RealEstates)
                .HasForeignKey(r => r.LandlordId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.AdminConfirmed)
                .WithMany(u => u.RealEstatesConfirmed)
                .HasForeignKey(r => r.AdminConfirmedId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
