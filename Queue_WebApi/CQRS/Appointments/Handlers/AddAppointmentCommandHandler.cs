using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.Auth;
using Queue_WebApi.CQRS.Appointments.Commands.CreateAppointments;
using Queue_WebApi.Models;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Handlers
{
    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, Guid>
    {
        private readonly QueueDbContext _dbContext;
        private readonly IValidator<AddAppointmentCommand> _validator;
        private readonly IUserProvider _userProvider;
        public AddAppointmentCommandHandler(QueueDbContext dbContext, IValidator<AddAppointmentCommand> validator, IUserProvider userProvider)
        {
            _dbContext = dbContext;
            _validator = validator;
            _userProvider = userProvider;
        }

        public async Task<Guid> Handle(AddAppointmentCommand command, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(command, cancellationToken);
            if (!validation.IsValid)
            {
                var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Ошибка валидации: {errors}");
            }

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken);
            try
            {
                bool isOccupied = await _dbContext.Apointsments.Where(a => !a.IsDeleted)
                    .AnyAsync(a => a.HallId == command.HallId &&
                                   (command.StartTime >= a.StartTime && command.StartTime < a.EndTime ||
                                    command.EndTime > a.StartTime && command.EndTime <= a.EndTime ||
                                    command.StartTime <= a.StartTime && command.EndTime >= a.EndTime),
                              cancellationToken);

                if (isOccupied)
                {
                    throw new InvalidOperationException("The time slot is already occupied.");
                }

                var hall = await _dbContext.Halls.FindAsync(command.HallId);
                if (hall == null) throw new Exception("Hall not found");

                var userId = _userProvider.GetUserId();
                if (!userId.HasValue) throw new Exception("User is not authenticated");

                var appointment = new Apointment
                {
                    Id = Guid.NewGuid(),
                    HallId = command.HallId,
                    UserId = userId.Value,
                    StartTime = command.StartTime,
                    EndTime = command.EndTime,
                    CreatedAt = DateTime.Now
                };

                _dbContext.Apointsments.Add(appointment);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return appointment.Id;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }

}
