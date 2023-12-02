using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastucture.Persistance.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           builder.HasKey(x => x.Id);

            builder.HasOne(c => c.Childern)
                   .WithOne(u => u.User)
                   .HasForeignKey<Childern>(x => x.UserId);

            builder.HasOne(c => c.Teacher)
                   .WithOne(u => u.User)
                   .HasForeignKey<Teacher>(x => x.UserId);

            builder.Property(u => u.UserName).IsUnicode();
        }
    }
}
