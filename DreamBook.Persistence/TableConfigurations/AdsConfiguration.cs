namespace DreamBook.Persistence.TableConfigurations
{
    public class AdsConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.ToTable(nameof(Ad));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasMany(p => p.Translations)
               .WithOne(p => p.Ad)
               .HasForeignKey(p => p.AdGuid)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
