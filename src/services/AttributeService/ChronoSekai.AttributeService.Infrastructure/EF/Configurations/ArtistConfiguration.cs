using ChronoSekai.AttributeService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoSekai.AttributeService.Infrastructure.EF.Configurations
{
    internal class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artists");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(a => a.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
