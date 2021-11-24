using Bogus;
using Portal.TM.Business.Extentions;

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
        var retorno = new Faker<Product>().CustomInstantiator(f =>
            new Product(f.Commerce.ProductName().Truncate(100), f.Commerce.ProductDescription().Truncate(100), f.Finance.Amount(1, 10000, 2)));
        return retorno;
    }
}
