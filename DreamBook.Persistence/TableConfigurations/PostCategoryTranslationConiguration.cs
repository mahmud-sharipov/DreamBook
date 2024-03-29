﻿using DreamBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamBook.Persistence.TableConfigurations
{
    public class PostCategoryTranslationConiguration : IEntityTypeConfiguration<PostCategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<PostCategoryTranslation> builder)
        {
            builder.ToTable(nameof(PostCategoryTranslation));
            builder.HasKey(p => p.Guid);
            builder.Property(p => p.Guid).ValueGeneratedOnAdd();

            builder.HasOne(p => p.PostCategory)
                .WithMany(p => p.Translations)
                .HasForeignKey(p => p.CategoryGuid)
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
