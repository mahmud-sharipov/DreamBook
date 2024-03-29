﻿// <auto-generated />
using System;
using DreamBook.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DreamBook.Persistence.Migrations.SqlServer
{
    [DbContext(typeof(DreamBookSqlServerContext))]
    [Migration("20220204193801_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DreamBook.Domain.Entities.Ad", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("Ad", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.AdTranslation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LanguageGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("AdGuid");

                    b.HasIndex("LanguageGuid");

                    b.ToTable("AdTranslation", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Book", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.BookTranslation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LanguageGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("BookGuid");

                    b.HasIndex("LanguageGuid");

                    b.ToTable("BookTranslation", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Dream", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CanBeShared")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MovedToRecycleBin")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfViews")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TypeGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Weather")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("AuthorGuid");

                    b.HasIndex("TypeGuid");

                    b.ToTable("Dream", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.DreamType", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("DreamType", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.DreamTypeTranslation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DreamTypeGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguageGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("DreamTypeGuid");

                    b.HasIndex("LanguageGuid");

                    b.ToTable("DreamTypeTranslation", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.DreamWord", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DreamGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WordGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.HasIndex("DreamGuid");

                    b.HasIndex("WordGuid");

                    b.ToTable("DreamWord", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Interpretation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WordGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.HasIndex("BookGuid");

                    b.HasIndex("WordGuid");

                    b.ToTable("Interpretation", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.InterpretationTranslation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookTranslationGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("InterpretationGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguageGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WordGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.HasIndex("BookGuid");

                    b.HasIndex("BookTranslationGuid");

                    b.HasIndex("InterpretationGuid");

                    b.HasIndex("LanguageGuid");

                    b.HasIndex("WordGuid");

                    b.ToTable("InterpretationTranslation", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Language", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("Language", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Post", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("CategoryGuid");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.PostCategory", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.ToTable("PostCategory", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.PostCategoryTranslation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LanguageGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("CategoryGuid");

                    b.HasIndex("LanguageGuid");

                    b.ToTable("PostCategoryTranslation", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Word", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.ToTable("Word", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.WordTranslation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguageGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WordGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.HasIndex("LanguageGuid");

                    b.HasIndex("WordGuid");

                    b.ToTable("WordTranslation", (string)null);
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.AdTranslation", b =>
                {
                    b.HasOne("DreamBook.Domain.Entities.Ad", "Ad")
                        .WithMany("Translations")
                        .HasForeignKey("AdGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ad");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.BookTranslation", b =>
                {
                    b.HasOne("DreamBook.Domain.Entities.Book", "Book")
                        .WithMany("Translations")
                        .HasForeignKey("BookGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Dream", b =>
                {
                    b.HasOne("DreamBook.Domain.Entities.User", "Author")
                        .WithMany("Dreams")
                        .HasForeignKey("AuthorGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.DreamType", "Type")
                        .WithMany("Dreams")
                        .HasForeignKey("TypeGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.DreamTypeTranslation", b =>
                {
                    b.HasOne("DreamBook.Domain.Entities.DreamType", "DreamType")
                        .WithMany("Translations")
                        .HasForeignKey("DreamTypeGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DreamType");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.DreamWord", b =>
                {
                    b.HasOne("DreamBook.Domain.Entities.Dream", "Dream")
                        .WithMany("Words")
                        .HasForeignKey("DreamGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.Word", "Word")
                        .WithMany("Dreams")
                        .HasForeignKey("WordGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Dream");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Interpretation", b =>
                {
                    b.HasOne("DreamBook.Domain.Entities.Book", "Book")
                        .WithMany("Interpretations")
                        .HasForeignKey("BookGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.Word", "Word")
                        .WithMany("Interpretations")
                        .HasForeignKey("WordGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.InterpretationTranslation", b =>
                {
                    b.HasOne("DreamBook.Domain.Entities.BookTranslation", "Book")
                        .WithMany()
                        .HasForeignKey("BookGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.BookTranslation", null)
                        .WithMany("Interpretations")
                        .HasForeignKey("BookTranslationGuid");

                    b.HasOne("DreamBook.Domain.Entities.Interpretation", "Interpretation")
                        .WithMany("Translations")
                        .HasForeignKey("InterpretationGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.WordTranslation", "Word")
                        .WithMany("Interpretations")
                        .HasForeignKey("WordGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Interpretation");

                    b.Navigation("Language");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Post", b =>
                {
                    b.HasOne("DreamBook.Domain.Entities.PostCategory", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.PostCategoryTranslation", b =>
                {
                    b.HasOne("DreamBook.Domain.Entities.PostCategory", "PostCategory")
                        .WithMany("Translations")
                        .HasForeignKey("CategoryGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("PostCategory");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.WordTranslation", b =>
                {
                    b.HasOne("DreamBook.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageGuid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DreamBook.Domain.Entities.Word", "Word")
                        .WithMany("Translations")
                        .HasForeignKey("WordGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Ad", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Book", b =>
                {
                    b.Navigation("Interpretations");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.BookTranslation", b =>
                {
                    b.Navigation("Interpretations");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Dream", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.DreamType", b =>
                {
                    b.Navigation("Dreams");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Interpretation", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.PostCategory", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.User", b =>
                {
                    b.Navigation("Dreams");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.Word", b =>
                {
                    b.Navigation("Dreams");

                    b.Navigation("Interpretations");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("DreamBook.Domain.Entities.WordTranslation", b =>
                {
                    b.Navigation("Interpretations");
                });
#pragma warning restore 612, 618
        }
    }
}
