using FluentValidation;

namespace Queue_WebApi.CQRS.Appointments.Commands
{
    public class AddAppointmentCommandValidator : AbstractValidator<AddAppointmentCommand>
    {
        public AddAppointmentCommandValidator()
        {
            RuleFor(x => x.HallId).NotEmpty().WithMessage("HallId is required.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.StartTime).LessThan(x => x.EndTime)
                .WithMessage("StartTime must be before EndTime.");
        }
    }

}
