using ChronoSekai.AttributeService.Application.Common;
using ChronoSekai.AttributeService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ChronoSekai.AttributeService.Infrastructure.EF.Contexts
{
    internal class AttributeDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public AttributeDbContext(DbContextOptions<AttributeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
