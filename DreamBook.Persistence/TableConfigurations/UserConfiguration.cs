namespace DreamBook.Persistence.TableConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{   
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(p => p.Dreams)
            .WithOne()
            .HasForeignKey(p => p.AuthorGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.RefreshTokens)
            .WithOne(t => t.User)
            .HasForeignKey(p => p.UserGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}