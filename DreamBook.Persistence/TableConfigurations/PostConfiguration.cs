namespace DreamBook.Persistence.TableConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable(nameof(Post));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasOne(p => p.Category)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.CategoryGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
