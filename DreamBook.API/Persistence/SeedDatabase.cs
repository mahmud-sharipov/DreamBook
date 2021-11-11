using DreamBook.API.Auth;
using DreamBook.API.Auth.Model;
using DreamBook.API.Infrastructure.Import;
using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DreamBook.API.Persistence
{
    public static class SeedDatabase
    {
        internal static Guid RuGuid => new Guid("943fee14-dba5-4195-9afe-20e4fe46f017");
        internal static Guid EnGuid => new Guid("3b7c7370-8759-400c-9624-e5fc947fb07c");

        public async static Task Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IContext>();
                CreateLanguages(context);
                Importer.ImportAll(context);

                CreateDreamTypes(context);
                CreatePostCategories(context);
                await CreateUserRoles(serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>());
                await CreateUser(serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>(), context);
            }
        }

        static void CreateLanguages(IContext context)
        {
            if (context.Count<Language>() > 0) return;

            context.AddRange(new[]
            {
                new Language()
                {
                    Guid = RuGuid,
                    Name = "Русский",
                    Code = "ru-RU",
                    IsDefault = true
                },
                new Language()
                {
                    Guid = EnGuid,
                    Name = "English",
                    Code = "en-EN"
                }
            });
            context.SaveChanges();
        }

        static void CreatePostCategories(IContext context)
        {
            if (context.Count<PostCategory>() > 0) return;

            context.AddRange(new[]
            {
                new PostCategory()
                {
                    Translations = new Collection<PostCategoryTranslation>()
                    {
                        new PostCategoryTranslation()
                        {
                            Name ="Tips for sleep",
                            LanguageGuid = EnGuid
                        },
                        new PostCategoryTranslation()
                        {
                            Name ="Советы для сна",
                            LanguageGuid = RuGuid
                        }
                    }
                },
                new PostCategory()
                {
                    Translations = new Collection<PostCategoryTranslation>()
                    {
                        new PostCategoryTranslation()
                        {
                            Name ="Interpretation",
                            LanguageGuid = EnGuid
                        },
                        new PostCategoryTranslation()
                        {
                            Name ="Толкование",
                            LanguageGuid = RuGuid
                        }
                    }
                },
                new PostCategory()
                {
                    Translations = new Collection<PostCategoryTranslation>()
                    {
                        new PostCategoryTranslation()
                        {
                            Name ="Solution",
                            LanguageGuid = EnGuid
                        },
                        new PostCategoryTranslation()
                        {
                            Name ="Решение",
                            LanguageGuid = RuGuid
                        }
                    }
                }
            });
        }

        static void CreateDreamTypes(IContext context)
        {
            if (context.Count<DreamType>() > 0) return;

            context.AddRange(new[]
            {
                new PostCategory()
                {
                    Translations = new Collection<PostCategoryTranslation>()
                    {
                        new PostCategoryTranslation()
                        {
                            Name ="Tips for sleep",
                            LanguageGuid = EnGuid
                        },
                        new PostCategoryTranslation()
                        {
                            Name ="Советы для сна",
                            LanguageGuid = RuGuid
                        }
                    }
                },
                new PostCategory()
                {
                    Translations = new Collection<PostCategoryTranslation>()
                    {
                        new PostCategoryTranslation()
                        {
                            Name ="Interpretation",
                            LanguageGuid = EnGuid
                        },
                        new PostCategoryTranslation()
                        {
                            Name ="Толкование",
                            LanguageGuid = RuGuid
                        }
                    }
                },
                new PostCategory()
                {
                    Translations = new Collection<PostCategoryTranslation>()
                    {
                        new PostCategoryTranslation()
                        {
                            Name ="Solution",
                            LanguageGuid = EnGuid
                        },
                        new PostCategoryTranslation()
                        {
                            Name ="Решение",
                            LanguageGuid = RuGuid
                        }
                    }
                }
            });

            context.AddRange(new[]
            {
                new DreamType()
                {
                    Color = "#27AE60",
                    Translations=new Collection<DreamTypeTranslation>()
                    {
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = RuGuid,
                            Name = "Хороший"
                        },
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = EnGuid,
                            Name = "Good"
                        },
                    }
                },
                new DreamType()
                {
                    Color = "#F2994A",
                    Translations=new Collection<DreamTypeTranslation>()
                    {
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = RuGuid,
                            Name = "Вещий"
                        },
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = EnGuid,
                            Name = "Prophetic"
                        },
                    }
                },
                new DreamType()
                {
                    Color = "#4F4F4F",
                    Translations=new Collection<DreamTypeTranslation>()
                    {
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = RuGuid,
                            Name = "Кошмарный"
                        },
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = EnGuid,
                            Name = "Nightmarish"
                        },
                    }
                },
                new DreamType()
                {
                    Color = "#F2C94C",
                    Translations=new Collection<DreamTypeTranslation>()
                    {
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = RuGuid,
                            Name = "Предупреждающий"
                        },
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = EnGuid,
                            Name = "Warning"
                        },
                    }
                },
                new DreamType()
                {
                    Color = "#EB5757",
                    Translations=new Collection<DreamTypeTranslation>()
                    {
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = RuGuid,
                            Name = "Плохой"
                        },
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = EnGuid,
                            Name = "Bad"
                        },
                    }
                },
                new DreamType()
                {
                    Color = "#2F80ED",
                    Translations=new Collection<DreamTypeTranslation>()
                    {
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = RuGuid,
                            Name = "Обычный"
                        },
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = EnGuid,
                            Name = "Normal"
                        },
                    }
                },
                new DreamType()
                {
                    Color = "#9B51E0",
                    Translations=new Collection<DreamTypeTranslation>()
                    {
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = RuGuid,
                            Name = "Необыкновенный"
                        },
                        new DreamTypeTranslation()
                        {
                            LanguageGuid = EnGuid,
                            Name = "Unusual"
                        },
                    }
                }
            });
            context.SaveChanges();
        }

        static async Task CreateUserRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new ApplicationRole() { Name = UserRoles.Admin });
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new ApplicationRole() { Name = UserRoles.User });
        }

        static async Task CreateUser(UserManager<ApplicationUser> userManager, IContext context)
        {
            var userExists = await userManager.FindByNameAsync("Admin");
            if (userExists == null)
            {
                var user = new User()
                {
                    Email = "test@user.account",
                    UserName = "admin",

                };
                context.Add(user);
                await context.SaveChangesAsync();

                ApplicationUser appUser = new ApplicationUser()
                {
                    Id = user.Guid,
                    Email = user.Email,
                    UserName = user.UserName
                };
                var result = await userManager.CreateAsync(appUser, "Test.1234");
                if (!result.Succeeded)
                    throw new Exception("Error on creating default user");

                await userManager.AddToRoleAsync(appUser, UserRoles.Admin);
            }
        }
    }
}
