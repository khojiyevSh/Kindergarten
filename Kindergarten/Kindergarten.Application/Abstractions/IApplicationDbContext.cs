using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<Childern> Childerns { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<GroupPrice> GroupPrices { get; set; }
        DbSet<Attendence> Attendences { get; set; }
        DbSet<TrainingTime> TrainingTimes { get; set; }
        DbSet<ChildernGroup> ChildernGroups { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
