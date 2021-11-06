using DreamBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamBook.Persistence.TableConfigurations
{
    public class DreamConfiguration : IEntityTypeConfiguration<Dream>
    {
        public void Configure(EntityTypeBuilder<Dream> builder)
        {
            builder.ToTable(nameof(Dream));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasOne(p => p.Type)
                .WithMany(p => p.Dreams)
                .HasForeignKey(p => p.TypeGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Author)
                .WithMany(p => p.Dreams)
                .HasForeignKey(p => p.AuthorGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Words)
               .WithOne(p => p.Dream)
               .HasForeignKey(p => p.DreamGuid)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
