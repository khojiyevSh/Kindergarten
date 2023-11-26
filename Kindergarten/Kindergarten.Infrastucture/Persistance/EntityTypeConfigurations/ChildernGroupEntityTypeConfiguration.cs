using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastucture.Persistance.EntityTypeConfigurations
{
    public class ChildernGroupEntityTypeConfiguration : IEntityTypeConfiguration<ChildernGroup>
    {
        public void Configure(EntityTypeBuilder<ChildernGroup> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
