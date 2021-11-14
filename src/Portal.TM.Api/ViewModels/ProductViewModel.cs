using System.ComponentModel.DataAnnotations;

namespace Portal.TM.Api.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
