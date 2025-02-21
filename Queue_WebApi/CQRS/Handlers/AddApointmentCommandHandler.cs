using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Commands;
using Queue_WebApi.DataBase;
using Queue_WebApi.Models;

namespace Queue_WebApi.CQRS.Handlers
{
    public class AddApointmentCommandHandler : IRequestHandler<AddApointmentCommand, int>
    {
        private readonly QueueDbContext _dbContext;
        private readonly IValidator<AddApointmentCommand> _validator;
        public AddApointmentCommandHandler(QueueDbContext queueDbContext, 
            IValidator<AddApointmentCommand> validator)
        {
            _validator = validator;
            _dbContext = queueDbContext;
        }
        public async Task<int> Handle(AddApointmentCommand command, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(command, cancellationToken);
            if (!validation.IsValid)
            {
                var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Ошибка валидации: {errors}");
            }
            await using var transaction =
                await _dbContext.Database.BeginTransactionAsync
                (
                    System.Data.IsolationLevel.Serializable, cancellationToken
                );
            try
            { bool isOccupied = await _dbContext.Apointsments
                .AnyAsync(a => a.QueueId == command.QueueId &&
                               ((command.StartTime >= a.StartTime && command.StartTime < a.EndTime) ||
                                (command.EndTime > a.StartTime && command.EndTime <= a.EndTime) ||
                                (command.StartTime <= a.StartTime && command.EndTime >= a.EndTime)),
                          cancellationToken);
                if (isOccupied)
                {
                    throw new InvalidOperationException("The time slot is already occupied.");
                }
                var client = await _dbContext.Clients.FindAsync(new object[] { command.ClientId });
                if (client == null) throw new Exception("Client not dound");
                var queue = await _dbContext.Queueas.FindAsync(command.QueueId);
                if (queue == null) throw new Exception("queuer not found");
                var apointment = new Apointment
                {
                    StartTime = command.StartTime,
                    EndTime = command.EndTime,
                    Description = command.Description,
                    ClientId = command.ClientId,
                    QueueId = command.QueueId
                };

                _dbContext.Apointsments.Add(apointment);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return apointment.Id;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
