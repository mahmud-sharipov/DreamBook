namespace A_Test_EF_Core
{
    public class Product
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }

        public Guid CategoryGuid { get; set; }
        public virtual ProductCategory Category { get; set; }
    }

    public class ProductCategory
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
