namespace DreamBook.Persistence.TableConfigurations
{
    public class InterpretationConfiguration : IEntityTypeConfiguration<Interpretation>
    {
        public void Configure(EntityTypeBuilder<Interpretation> builder)
        {
            builder.ToTable(nameof(Interpretation));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasMany(p => p.Translations)
                .WithOne(p => p.Interpretation)
                .HasForeignKey(p => p.InterpretationGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Book)
                 .WithMany(p => p.Interpretations)
                 .HasForeignKey(p => p.BookGuid)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Word)
                 .WithMany(p => p.Interpretations)
                 .HasForeignKey(p => p.WordGuid)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
