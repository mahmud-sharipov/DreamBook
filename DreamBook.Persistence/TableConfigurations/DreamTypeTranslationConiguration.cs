using DreamBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamBook.Persistence.TableConfigurations
{
    public class DreamTypeTranslationConiguration : IEntityTypeConfiguration<DreamTypeTranslation>
    {
        public void Configure(EntityTypeBuilder<DreamTypeTranslation> builder)
        {
            builder.ToTable(nameof(DreamTypeTranslation));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasOne(p => p.DreamType)
               .WithMany(p => p.Translations)
               .HasForeignKey(p => p.DreamTypeGuid)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Language)
               .WithMany()
               .HasForeignKey(p => p.LanguageGuid)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
