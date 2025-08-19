using ChronoSekai.StatusService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoSekai.StatusService.Infrastructure.EF.Configurations
{
    internal sealed class TypeTitleConfiguration : IEntityTypeConfiguration<TypeTitle>
    {
        public void Configure(EntityTypeBuilder<TypeTitle> builder)
        {
            builder.ToTable("TypeTitles");
            builder.HasKey(st => st.Id);

            builder.Property(st => st.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(st => st.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
