using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastucture.Persistance.EntityTypeConfigurations
{
    public class ChildernEntityTypeConfiguration : IEntityTypeConfiguration<Childern>
    {
        public void Configure(EntityTypeBuilder<Childern> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(c => c.ChildernGroups)
                  .WithOne(c => c.Childern)
                  .HasForeignKey(c => c.ChildernId);

            builder.HasMany(a=>a.Attendences)
                  .WithOne(c => c.Childern)
                  .HasForeignKey(c => c.ChildernId);
        }
    }
}
