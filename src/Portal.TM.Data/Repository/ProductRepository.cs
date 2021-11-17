using Portal.TM.Business.Entities;
using Portal.TM.Business.Interfaces;
using Portal.TM.Data.Context;

namespace Portal.TM.Data.Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(MyDbContext context) : base(context)
    {
    }
}
