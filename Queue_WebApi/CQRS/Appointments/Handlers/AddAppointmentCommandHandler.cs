using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Appointments.Commands;
using Queue_WebApi.Models;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Handlers
{
    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, Guid>
    {
        private readonly QueueDbContext _dbContext;
        private readonly IValidator<AddAppointmentCommand> _validator;

        public AddAppointmentCommandHandler(QueueDbContext dbContext, IValidator<AddAppointmentCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
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
                bool isOccupied = await _dbContext.Apointsments
                    .AnyAsync(a => a.HallId == command.HallId &&
                                   ((command.StartTime >= a.StartTime && command.StartTime < a.EndTime) ||
                                    (command.EndTime > a.StartTime && command.EndTime <= a.EndTime) ||
                                    (command.StartTime <= a.StartTime && command.EndTime >= a.EndTime)),
                              cancellationToken);

                if (isOccupied)
                {
                    throw new InvalidOperationException("The time slot is already occupied.");
                }

                var hall = await _dbContext.Halls.FindAsync(command.HallId);
                if (hall == null) throw new Exception("Hall not found");

                var user = await _dbContext.Users.FindAsync(command.UserId);
                if (user == null) throw new Exception("User not found");

                var appointment = new Apointment
                {
                    Id = Guid.NewGuid(),
                    HallId = command.HallId,
                    UserId = command.UserId,
                    StartTime = command.StartTime,
                    EndTime = command.EndTime
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
