namespace DreamBook.Persistence.TableConfigurations
{
    public class WordTranslationConfiguration : IEntityTypeConfiguration<WordTranslation>
    {
        public void Configure(EntityTypeBuilder<WordTranslation> builder)
        {
            builder.ToTable(nameof(WordTranslation));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(p => p.Word)
                .WithMany(p => p.Translations)
                .HasForeignKey(p => p.WordGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Language)
               .WithMany()
               .HasForeignKey(p => p.LanguageGuid)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Interpretations)
                .WithOne(p => p.Word)
                .HasForeignKey(p => p.WordGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
