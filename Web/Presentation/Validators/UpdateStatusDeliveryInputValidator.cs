namespace WebHook.WebHook.MinimalApi.Validators;

using FluentValidation;
using global::WebHook.Data.Models.GiaoHangTietKiem.Dtos;

public class UpdateStatusDeliveryInputValidator : AbstractValidator<UpdateStatusDeliveryInput>
{
    public UpdateStatusDeliveryInputValidator()
    {
        _ = RuleFor(r => r.PartnerId).NotEmpty().WithMessage("PartnerId is require");
    }
}
