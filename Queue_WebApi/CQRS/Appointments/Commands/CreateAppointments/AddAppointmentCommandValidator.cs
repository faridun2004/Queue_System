using FluentValidation;

namespace Queue_WebApi.CQRS.Appointments.Commands.CreateAppointments
{
    public class AddAppointmentCommandValidator : AbstractValidator<AddAppointmentCommand>
    {
        public AddAppointmentCommandValidator()
        {
            RuleFor(x => x.HallId).NotEmpty().WithMessage("HallId is required.");
            RuleFor(x => x.StartTime).LessThan(x => x.EndTime)
                .WithMessage("StartTime must be before EndTime.");
        }
    }

}
