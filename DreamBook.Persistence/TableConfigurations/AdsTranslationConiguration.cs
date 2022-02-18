namespace DreamBook.Persistence.TableConfigurations
{
    public class AdsTranslationConiguration : IEntityTypeConfiguration<AdTranslation>
    {
        public void Configure(EntityTypeBuilder<AdTranslation> builder)
        {
            builder.ToTable(nameof(AdTranslation));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasOne(p => p.Ad)
               .WithMany(p => p.Translations) 
               .HasForeignKey(p => p.AdGuid)
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
