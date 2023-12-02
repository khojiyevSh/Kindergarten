using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastucture.Persistance.EntityTypeConfigurations
{
    internal class TrainingTimeEntityTypeConfiguration : IEntityTypeConfiguration<TrainingTime>
    {
        public void Configure(EntityTypeBuilder<TrainingTime> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(a => a.Attendences)
                   .WithOne(t => t.TrainingTime)
                   .HasForeignKey(t => t.TrainingTimeId);
        }
    }
}
