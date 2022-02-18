namespace DreamBook.Persistence.TableConfigurations
{
    public class BookTranslationConfiguration : IEntityTypeConfiguration<BookTranslation>
    {
        public void Configure(EntityTypeBuilder<BookTranslation> builder)
        {
            builder.ToTable(nameof(BookTranslation));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(p => p.Book)
                .WithMany(p => p.Translations)
                .HasForeignKey(p => p.BookGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Language)
               .WithMany()
               .HasForeignKey(p => p.LanguageGuid)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Interpretations)
                .WithOne(p => p.Book)
                .HasForeignKey(p => p.WordGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
