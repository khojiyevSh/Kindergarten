using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastucture.Persistance.EntityTypeConfigurations
{
    public class TeacherEntityTypeConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(x=> x.Id);

            builder.HasMany(g => g.Groups)
                  .WithOne(t => t.Teacher)
                  .HasForeignKey(t => t.TeacherId);
        }
    }
}
