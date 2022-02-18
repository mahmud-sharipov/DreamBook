namespace DreamBook.Persistence.TableConfigurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(nameof(RefreshToken));
        builder.HasKey(p => p.Guid);
        builder.Property(p => p.Guid).ValueGeneratedOnAdd();

        builder.HasOne(p => p.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(p => p.UserGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
