using Bogus;

namespace Portal.TM.Business.Entities;
public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime DateCreation { get; set; }

    public Product()
    {
        
    }

    public Product(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
        DateCreation = DateTime.UtcNow;
    }

    public static Faker<Product> Get()
    {
        return new Faker<Product>().CustomInstantiator(f =>
            new Product(f.Commerce.ProductName(), f.Commerce.ProductDescription(), f.Finance.Amount(1, 10000,2)));
    }
}
