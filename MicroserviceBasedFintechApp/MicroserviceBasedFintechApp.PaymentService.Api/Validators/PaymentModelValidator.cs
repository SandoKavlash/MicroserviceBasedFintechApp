using FluentValidation;
using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Models;

namespace MicroserviceBasedFintechApp.PaymentService.Api.Validators
{
    public class PaymentModelValidator : AbstractValidator<PaymentModel>
    {
        public PaymentModelValidator()
        {
            RuleFor(x => x.CardNumber)
            .CreditCard()
            .WithMessage("Invalid credit card number.");

            RuleFor(x => x.CardExpirationDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Card expiration date must be in the future.");
        }
    }
}
