using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamBook.Persistence.Migrations.MySql
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ad",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Source = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ad", x => x.Guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DreamType",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DreamType", x => x.Guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PostCategory",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategory", x => x.Guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AvatarImage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LanguageGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AdGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdTranslation", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_AdTranslation_Ad_AdGuid",
                        column: x => x.AdGuid,
                        principalTable: "Ad",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdTranslation_Language_LanguageGuid",
                        column: x => x.LanguageGuid,
                        principalTable: "Language",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BookTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LanguageGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTranslation", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_BookTranslation_Book_BookGuid",
                        column: x => x.BookGuid,
                        principalTable: "Book",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTranslation_Language_LanguageGuid",
                        column: x => x.LanguageGuid,
                        principalTable: "Language",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DreamTypeTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DreamTypeGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LanguageGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DreamTypeTranslation", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_DreamTypeTranslation_DreamType_DreamTypeGuid",
                        column: x => x.DreamTypeGuid,
                        principalTable: "DreamType",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DreamTypeTranslation_Language_LanguageGuid",
                        column: x => x.LanguageGuid,
                        principalTable: "Language",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CategoryGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Post_PostCategory_CategoryGuid",
                        column: x => x.CategoryGuid,
                        principalTable: "PostCategory",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PostCategoryTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LanguageGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategoryTranslation", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_PostCategoryTranslation_Language_LanguageGuid",
                        column: x => x.LanguageGuid,
                        principalTable: "Language",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostCategoryTranslation_PostCategory_CategoryGuid",
                        column: x => x.CategoryGuid,
                        principalTable: "PostCategory",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Dream",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weather = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumberOfViews = table.Column<int>(type: "int", nullable: false),
                    CanBeShared = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MovedToRecycleBin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AuthorGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TypeGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dream", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Dream_DreamType_TypeGuid",
                        column: x => x.TypeGuid,
                        principalTable: "DreamType",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dream_User_AuthorGuid",
                        column: x => x.AuthorGuid,
                        principalTable: "User",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Interpretation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WordGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interpretation", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Interpretation_Book_BookGuid",
                        column: x => x.BookGuid,
                        principalTable: "Book",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interpretation_Word_WordGuid",
                        column: x => x.WordGuid,
                        principalTable: "Word",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WordTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WordGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LanguageGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordTranslation", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_WordTranslation_Language_LanguageGuid",
                        column: x => x.LanguageGuid,
                        principalTable: "Language",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WordTranslation_Word_WordGuid",
                        column: x => x.WordGuid,
                        principalTable: "Word",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DreamWord",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DreamGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WordGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DreamWord", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_DreamWord_Dream_DreamGuid",
                        column: x => x.DreamGuid,
                        principalTable: "Dream",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DreamWord_Word_WordGuid",
                        column: x => x.WordGuid,
                        principalTable: "Word",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InterpretationTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InterpretationGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LanguageGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WordGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookTranslationGuid = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterpretationTranslation", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_InterpretationTranslation_BookTranslation_BookGuid",
                        column: x => x.BookGuid,
                        principalTable: "BookTranslation",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterpretationTranslation_BookTranslation_BookTranslationGuid",
                        column: x => x.BookTranslationGuid,
                        principalTable: "BookTranslation",
                        principalColumn: "Guid");
                    table.ForeignKey(
                        name: "FK_InterpretationTranslation_Interpretation_InterpretationGuid",
                        column: x => x.InterpretationGuid,
                        principalTable: "Interpretation",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterpretationTranslation_Language_LanguageGuid",
                        column: x => x.LanguageGuid,
                        principalTable: "Language",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterpretationTranslation_WordTranslation_WordGuid",
                        column: x => x.WordGuid,
                        principalTable: "WordTranslation",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AdTranslation_AdGuid",
                table: "AdTranslation",
                column: "AdGuid");

            migrationBuilder.CreateIndex(
                name: "IX_AdTranslation_LanguageGuid",
                table: "AdTranslation",
                column: "LanguageGuid");

            migrationBuilder.CreateIndex(
                name: "IX_BookTranslation_BookGuid",
                table: "BookTranslation",
                column: "BookGuid");

            migrationBuilder.CreateIndex(
                name: "IX_BookTranslation_LanguageGuid",
                table: "BookTranslation",
                column: "LanguageGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Dream_AuthorGuid",
                table: "Dream",
                column: "AuthorGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Dream_TypeGuid",
                table: "Dream",
                column: "TypeGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DreamTypeTranslation_DreamTypeGuid",
                table: "DreamTypeTranslation",
                column: "DreamTypeGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DreamTypeTranslation_LanguageGuid",
                table: "DreamTypeTranslation",
                column: "LanguageGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DreamWord_DreamGuid",
                table: "DreamWord",
                column: "DreamGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DreamWord_WordGuid",
                table: "DreamWord",
                column: "WordGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Interpretation_BookGuid",
                table: "Interpretation",
                column: "BookGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Interpretation_WordGuid",
                table: "Interpretation",
                column: "WordGuid");

            migrationBuilder.CreateIndex(
                name: "IX_InterpretationTranslation_BookGuid",
                table: "InterpretationTranslation",
                column: "BookGuid");

            migrationBuilder.CreateIndex(
                name: "IX_InterpretationTranslation_BookTranslationGuid",
                table: "InterpretationTranslation",
                column: "BookTranslationGuid");

            migrationBuilder.CreateIndex(
                name: "IX_InterpretationTranslation_InterpretationGuid",
                table: "InterpretationTranslation",
                column: "InterpretationGuid");

            migrationBuilder.CreateIndex(
                name: "IX_InterpretationTranslation_LanguageGuid",
                table: "InterpretationTranslation",
                column: "LanguageGuid");

            migrationBuilder.CreateIndex(
                name: "IX_InterpretationTranslation_WordGuid",
                table: "InterpretationTranslation",
                column: "WordGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CategoryGuid",
                table: "Post",
                column: "CategoryGuid");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategoryTranslation_CategoryGuid",
                table: "PostCategoryTranslation",
                column: "CategoryGuid");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategoryTranslation_LanguageGuid",
                table: "PostCategoryTranslation",
                column: "LanguageGuid");

            migrationBuilder.CreateIndex(
                name: "IX_WordTranslation_LanguageGuid",
                table: "WordTranslation",
                column: "LanguageGuid");

            migrationBuilder.CreateIndex(
                name: "IX_WordTranslation_WordGuid",
                table: "WordTranslation",
                column: "WordGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdTranslation");

            migrationBuilder.DropTable(
                name: "DreamTypeTranslation");

            migrationBuilder.DropTable(
                name: "DreamWord");

            migrationBuilder.DropTable(
                name: "InterpretationTranslation");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "PostCategoryTranslation");

            migrationBuilder.DropTable(
                name: "Ad");

            migrationBuilder.DropTable(
                name: "Dream");

            migrationBuilder.DropTable(
                name: "BookTranslation");

            migrationBuilder.DropTable(
                name: "Interpretation");

            migrationBuilder.DropTable(
                name: "WordTranslation");

            migrationBuilder.DropTable(
                name: "PostCategory");

            migrationBuilder.DropTable(
                name: "DreamType");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Word");
        }
    }
}
