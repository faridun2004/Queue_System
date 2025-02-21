using FluentValidation;
using Queue_WebApi.CQRS.Commands;
namespace Queue_WebApi.CQRS.Handlers
{
    public class AddApointmentCommandValidator : AbstractValidator<AddApointmentCommand>
    {
        public AddApointmentCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("ClientId dont empty");

            RuleFor(x => x.QueueId)
                .NotEmpty().WithMessage("QueueId dont empty");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("StartTime is reuiered")
                .LessThan(x => x.EndTime).WithMessage("StartTime should be earlier EndTime.")
                .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("StartTime cannot be in the past.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("EndTime is reuqired.")
                .GreaterThan(x => x.StartTime).WithMessage("EndTime should be later StartTime.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }

}
