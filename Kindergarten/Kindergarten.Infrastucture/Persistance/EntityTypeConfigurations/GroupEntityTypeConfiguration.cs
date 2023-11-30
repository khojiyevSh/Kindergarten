using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastucture.Persistance.EntityTypeConfigurations
{
    internal class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(g => g.GroupPrices)
                   .WithOne(g => g.Group)
                   .HasForeignKey(g => g.GroupId); 
            
            builder.HasMany(t=>t.TrainingTimes)
                   .WithOne(g => g.Group)
                   .HasForeignKey(g => g.GroupId);

            builder.HasMany(c=>c.ChildernGroups)
                   .WithOne(g => g.Group)
                   .HasForeignKey(g => g.GroupId);

            builder.Property(x => x.Name).IsUnicode();
        }
    }
}
