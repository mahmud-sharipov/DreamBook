using DreamBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamBook.Persistence.TableConfigurations
{
    public class PostCategoryConfiguration : IEntityTypeConfiguration<PostCategory>
    {
        public void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            builder.ToTable(nameof(PostCategory));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasMany(p => p.Posts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Translations)
                .WithOne(p => p.PostCategory)
                .HasForeignKey(p => p.CategoryGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
