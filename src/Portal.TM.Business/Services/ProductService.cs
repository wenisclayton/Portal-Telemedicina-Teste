using AspNetCore.IQueryable.Extensions;
using Portal.TM.Business.Entities;
using Portal.TM.Business.Interfaces;
using Portal.TM.Business.Notifications;

namespace Portal.TM.Business.Services;
public class ProductService : BaseService, IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IDomainNotificationMediatorService _domainNotification;

    public ProductService(
        IDomainNotificationMediatorService domainNotification,
        IProductRepository productRepository) : base(domainNotification)
    {
        _domainNotification = domainNotification;
        _productRepository = productRepository;
    }

    public IQueryable<Product> Query(ICustomQueryable search)
    {
        CheckProduct().Wait();
        return _productRepository.Queryable().Apply(search);
    }

    public async Task<Product?> Save(Product product)
    {
        product.DateCreation = DateTime.Now;
        await _productRepository.AddAsync(product);

        var newProduct = await _productRepository.GetByIdAsync(product.Id);

        return newProduct;
    }

    public async Task Update(Guid id, Product product)
    {
        var currentProduct = await _productRepository.GetByIdAsync(id);
        if (currentProduct == null)
        {
            _domainNotification.Notify(new DomainNotification("Product", $"Product '{id}' not found"));
        }
        else
        {
            currentProduct.Description = product.Description;
            currentProduct.Name = product.Name;
            currentProduct.Price = product.Price;
           await _productRepository.UpdateAsync(currentProduct);
        }
    }

    private async Task CheckProduct()
    {
        if (_productRepository.Queryable().Any())
            return;

        var products = Product.Get().Generate(5);

        await _productRepository.AddRangeAsync(products);
    }
}

public interface IProductService
{
    public IQueryable<Product> Query(ICustomQueryable search);
    Task<Product?> Save(Product product);
    Task Update(Guid id, Product product);
}
