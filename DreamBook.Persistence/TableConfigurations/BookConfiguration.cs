using DreamBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamBook.Persistence.TableConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasMany(p => p.Translations)
                .WithOne(p => p.Book)
                .HasForeignKey(p => p.BookGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Interpretations)
                .WithOne(p => p.Book)
                .HasForeignKey(p => p.BookGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
