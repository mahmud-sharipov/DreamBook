namespace DreamBook.Persistence.TableConfigurations
{
    public class InterpretationTranslationConfiguration : IEntityTypeConfiguration<InterpretationTranslation>
    {
        public void Configure(EntityTypeBuilder<InterpretationTranslation> builder)
        {
            builder.ToTable(nameof(InterpretationTranslation));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasOne(p => p.Interpretation)
                .WithMany(p => p.Translations)
                .HasForeignKey(p => p.InterpretationGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Language)
               .WithMany()
               .HasForeignKey(p => p.LanguageGuid)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Word)
               .WithMany()
               .HasForeignKey(p => p.WordGuid)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Book)
               .WithMany()
               .HasForeignKey(p => p.BookGuid)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
