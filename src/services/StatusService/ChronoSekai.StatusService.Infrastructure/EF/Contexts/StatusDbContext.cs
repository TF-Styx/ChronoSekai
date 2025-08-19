using ChronoSekai.StatusService.Application.Common;
using ChronoSekai.StatusService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ChronoSekai.StatusService.Infrastructure.EF.Contexts
{
    internal class StatusDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<StatusTitle> StatusTitles { get; set; }
        public DbSet<StatusTranslate> StatusTranslates { get; set; }
        public DbSet<TypeTitle> TypeTitles { get; set; }

        public StatusDbContext(DbContextOptions<StatusDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
