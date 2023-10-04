namespace WebHook.WebHook.MinimalApi.Validators;

using FluentValidation;

public class GenericIdentityValidator : AbstractValidator<Guid>
{
    public GenericIdentityValidator()
    {
        _ = RuleFor(r => r).NotEqual(Guid.Empty).WithMessage("A valid Id was not supplied.");
    }
}
