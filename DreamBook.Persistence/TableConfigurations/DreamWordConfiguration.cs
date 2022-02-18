namespace DreamBook.Persistence.TableConfigurations
{
    public class DreamWordConfiguration : IEntityTypeConfiguration<DreamWord>
    {
        public void Configure(EntityTypeBuilder<DreamWord> builder)
        {
            builder.ToTable(nameof(DreamWord));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasOne(p => p.Word)
                .WithMany(p => p.Dreams)
                .HasForeignKey(p => p.WordGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Dream)
                .WithMany(p => p.Words)
                .HasForeignKey(p => p.DreamGuid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
