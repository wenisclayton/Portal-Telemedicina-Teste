using MediatR;
using Portal.TM.Business.Interfaces;

namespace Portal.TM.Business.Notifications;
public class DomainNotificationMediatorService : IDomainNotificationMediatorService
{
    private readonly IMediator _mediator;

    public DomainNotificationMediatorService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void Notify(DomainNotification notify)
    {
        _mediator.Publish(notify);
    }
}