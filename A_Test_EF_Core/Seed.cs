using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Test_EF_Core
{
    public class Seed
    {
        public Seed()
        {
            CreateDataBase();
            EnterRecord();
        }

        public void CreateDataBase()
        {
            using (var context = new Context())
            {
                context.Database.EnsureCreated();
            }
        }

        public void EnterRecord()
        {
            using (var context = new Context())
            {
                for (int i = 0; i < 100000; i++)
                {
                    var category = new ProductCategory() { Name = $"Category {i}", Guid = Guid.NewGuid() };
                    context.Add(category);
                    for (int j = 0; j < 200; j++)
                    {
                        context.Add(new Product()
                        {
                            Name = "Name1: " + j,
                            Name2 = "Name2: " + j,
                            Name3 = "Name3: " + j,
                            Name4 = "Name4: " + j,
                            Guid = Guid.NewGuid(),
                            CategoryGuid = category.Guid,
                        });
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
