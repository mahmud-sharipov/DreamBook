using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DreamBook.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ad",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ad", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "DreamType",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DreamType", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PostCategory",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategory", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvatarImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "AdTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "BookTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "DreamTypeTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DreamTypeGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "PostCategoryTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Dream",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weather = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfViews = table.Column<int>(type: "int", nullable: false),
                    CanBeShared = table.Column<bool>(type: "bit", nullable: false),
                    MovedToRecycleBin = table.Column<bool>(type: "bit", nullable: false),
                    AuthorGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Interpretation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WordGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "WordTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WordGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "DreamWord",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DreamGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WordGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "InterpretationTranslation",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InterpretationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WordGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookTranslationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
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
                });

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
