using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmailOtpRepository : EfRepositoryBase<EmailOtp, Guid, BaseDbContext>, IEmailOtpRepository
{
    public EmailOtpRepository(BaseDbContext context)
        : base(context) { }
}
