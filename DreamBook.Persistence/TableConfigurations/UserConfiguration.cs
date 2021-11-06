using DreamBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamBook.Persistence.TableConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasMany(p => p.Dreams)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
