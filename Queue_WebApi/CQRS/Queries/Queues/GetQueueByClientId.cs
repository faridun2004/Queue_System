//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Queue_WebApi.DTOs;
//using Queue_WebApi.Persistence.DataBase;

//namespace Queue_WebApi.CQRS.Queries.Queues
//{
//    public record GetAppointmentsByClientIdQuery(int ClientId) : IRequest<IEnumerable<ApointmentDto>>;



//    public class GetAppointmentsByClientIdQueryHandler : IRequestHandler<GetAppointmentsByClientIdQuery, IEnumerable<ApointmentDto>>
//    {
//        private readonly QueueDbContext _context;

//        public GetAppointmentsByClientIdQueryHandler(QueueDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<ApointmentDto>> Handle(GetAppointmentsByClientIdQuery request, CancellationToken cancellationToken)
//        {
//            var appointments = await _context.Apointsments
//                .Where(a => a.ClientId == request.ClientId)
//                .Include(a => a.Client)
//                .Select(a => new ApointmentDto
//                {
//                    Id = a.Id,
//                    StartTime = a.StartTime,
//                    EndTime = a.EndTime,
//                    Description = a.Description,
//                    ClientId = a.ClientId,
//                    ClientName = a.Client.Name,
//                    QueueId = a.QueueId
//                })
//                .ToListAsync(cancellationToken);

//            return appointments;
//        }
//    }
//}




