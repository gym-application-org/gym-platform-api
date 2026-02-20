using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SupportTicketRepository : EfRepositoryBase<SupportTicket, int, BaseDbContext>, ISupportTicketRepository
{
    public SupportTicketRepository(BaseDbContext context)
        : base(context) { }
}
