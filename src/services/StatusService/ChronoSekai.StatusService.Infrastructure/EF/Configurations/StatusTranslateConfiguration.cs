using ChronoSekai.StatusService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoSekai.StatusService.Infrastructure.EF.Configurations
{
    internal sealed class StatusTranslateConfiguration : IEntityTypeConfiguration<StatusTranslate>
    {
        public void Configure(EntityTypeBuilder<StatusTranslate> builder)
        {
            builder.ToTable("StatusTranslates");
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
