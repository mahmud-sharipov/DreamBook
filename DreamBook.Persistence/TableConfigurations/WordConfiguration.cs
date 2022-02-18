namespace DreamBook.Persistence.TableConfigurations
{
    public class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.ToTable(nameof(Word));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasMany(p => p.Translations)
                .WithOne(p => p.Word)
                .HasForeignKey(p => p.WordGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Interpretations)
                .WithOne(p => p.Word)
                .HasForeignKey(p => p.WordGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Dreams)
                .WithOne(p => p.Word)
                .HasForeignKey(p => p.WordGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
