using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace Portal.TM.Api.ViewModels;
public class ProductRegister
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
