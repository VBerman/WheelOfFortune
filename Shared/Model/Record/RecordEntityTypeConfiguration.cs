using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.Model.Record
{
    internal class RecordEntityTypeConfiguration : IEntityTypeConfiguration<RecordEntity>
    {
        public void Configure(EntityTypeBuilder<RecordEntity> entity)
        {
            entity.HasOne(r => r.Client)
                .WithMany(u => u.Records)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(r => r.RealEstate)
                .WithMany(r => r.Records)
                .HasForeignKey(r => r.RealEstateId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
