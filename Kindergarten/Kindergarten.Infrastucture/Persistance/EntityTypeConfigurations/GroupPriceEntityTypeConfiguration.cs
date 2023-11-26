using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastucture.Persistance.EntityTypeConfigurations
{
    public class GroupPriceEntityTypeConfiguration : IEntityTypeConfiguration<GroupPrice>
    {
        public void Configure(EntityTypeBuilder<GroupPrice> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
