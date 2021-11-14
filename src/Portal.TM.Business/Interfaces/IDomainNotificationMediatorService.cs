using Portal.TM.Business.Notifications;

namespace Portal.TM.Business.Interfaces; 
public interface IDomainNotificationMediatorService
{
    void Notify(DomainNotification notify);
}
