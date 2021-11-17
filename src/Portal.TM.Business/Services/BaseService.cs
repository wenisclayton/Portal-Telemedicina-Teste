using FluentValidation;
using FluentValidation.Results;
using Portal.TM.Business.Entities;
using Portal.TM.Business.Interfaces;
using Portal.TM.Business.Notifications;

namespace Portal.TM.Business.Services;
public abstract class BaseService
{
    private readonly IDomainNotificationMediatorService _domainNotification;

    protected BaseService(IDomainNotificationMediatorService domainNotification)
    {
        _domainNotification = domainNotification;
    }

    protected void Notificar(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notificar(error.PropertyName, error.ErrorMessage);
        }
    }

    protected void Notificar(string property, string mensagem)
    {
        _domainNotification.Notify(new DomainNotification(property, mensagem));
    }


    protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : BaseEntity
    {
        var validator = validacao.Validate(entidade);

        if (validator.IsValid) return true;

        Notificar(validator);

        return false;
    }
}
