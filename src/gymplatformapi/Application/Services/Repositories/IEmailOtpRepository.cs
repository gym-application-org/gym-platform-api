using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IEmailOtpRepository : IAsyncRepository<EmailOtp, Guid>, IRepository<EmailOtp, Guid> { }
