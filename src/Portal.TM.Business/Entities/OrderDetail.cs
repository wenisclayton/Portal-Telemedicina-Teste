namespace Portal.TM.Business.Entities;

public class OrderDetail : BaseEntity
{
    public Order Order { get; set; }
    public Product Product { get; set; }
    public short Quantity { get; set; }
}
