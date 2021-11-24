using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portal.TM.Api.ViewModels;
using Portal.TM.Business.Entities;
using Portal.TM.Business.Interfaces;
using Portal.TM.Business.Notifications;
using Portal.TM.Business.Services;

namespace Portal.TM.Api.Controllers;

[Route("api/product")]
public class ProductController : MainBaseController
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public ProductController(
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications,
        IDomainNotificationMediatorService mediator,
        IProductService productService) : base(notifications, mediator)
    {
        _mapper = mapper;
        _productService = productService;
    }

    [HttpGet("")]
    public async Task<ActionResult<List<ProductViewModel>>> GetAll([FromQuery] ProductSearch search)
    {
        var result = _productService.Query(search);
        var lstProducts = await _mapper.ProjectTo<ProductViewModel>(result).ToListAsync();
        return ResponseGet(lstProducts);
    }

    [HttpPost("")]
    public async Task<ActionResult<Product>> Post([FromBody] ProductRegister productRegister)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return ModelStateErrorResponseError();
        }
        var newUser = await _productService.Save(_mapper.Map<Product>(productRegister));
        if (newUser == null)
        {
            NotifyError("", "Unable to recover saved product");
            return ModelStateErrorResponseError();
        }

        return ResponsePost(nameof(Post), new { name = productRegister.Name }, newUser);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] ProductRegister productUpdate)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return ModelStateErrorResponseError();
        }

        var product = _mapper.Map<Product>(productUpdate);
        await _productService.Update(id, product);
        return ResponsePutPatch();
    }

}

