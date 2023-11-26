using Kindergarten.Application.Abstractions;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastucture.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public DbSet<User>? Users { get; set; }
        public DbSet<Teacher>? Teachers { get; set; }
        public DbSet<Childern>? Childerns { get; set; }
        public DbSet<Group>? Groups { get; set; }
        public DbSet<GroupPrice>? GroupPrices { get; set; }
        public DbSet<Attendence>? Attendences { get; set; }
        public DbSet<TrainingTime>? TrainingTimes { get; set; }
        public DbSet<ChildernGroup>? ChildernGroups { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }
    }
}
