namespace Portal.TM.Business.Models;

public class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
}

