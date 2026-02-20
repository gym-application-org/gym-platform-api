using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ISupportTicketRepository : IAsyncRepository<SupportTicket, int>, IRepository<SupportTicket, int> { }
