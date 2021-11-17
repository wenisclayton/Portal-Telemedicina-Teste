namespace Portal.TM.Business.Entities;
public class Order : BaseEntity
{
    public ICollection<OrderDetail> OrderDetails { get; set; }
}