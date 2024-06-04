using Domain;
using FluentValidation;

namespace Application.Orders
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.OrderNumber).NotEmpty();
        }
    }
}
