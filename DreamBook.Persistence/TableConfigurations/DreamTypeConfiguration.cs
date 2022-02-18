namespace DreamBook.Persistence.TableConfigurations
{
    public class DreamTypeConfiguration : IEntityTypeConfiguration<DreamType>
    {
        public void Configure(EntityTypeBuilder<DreamType> builder)
        {
            builder.ToTable(nameof(DreamType));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasMany(p => p.Dreams)
                .WithOne(p => p.Type)
                .HasForeignKey(p => p.TypeGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Translations)
               .WithOne(p => p.DreamType)
               .HasForeignKey(p => p.DreamTypeGuid)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
