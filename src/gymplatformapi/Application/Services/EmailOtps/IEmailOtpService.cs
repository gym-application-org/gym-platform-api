using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.EmailOtps;

public interface IEmailOtpService
{
    Task<EmailOtp?> GetAsync(
        Expression<Func<EmailOtp, bool>> predicate,
        Func<IQueryable<EmailOtp>, IIncludableQueryable<EmailOtp, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<EmailOtp>?> GetListAsync(
        Expression<Func<EmailOtp, bool>>? predicate = null,
        Func<IQueryable<EmailOtp>, IOrderedQueryable<EmailOtp>>? orderBy = null,
        Func<IQueryable<EmailOtp>, IIncludableQueryable<EmailOtp, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<EmailOtp> AddAsync(EmailOtp emailOtp);
    Task<EmailOtp> UpdateAsync(EmailOtp emailOtp);
    Task<EmailOtp> DeleteAsync(EmailOtp emailOtp, bool permanent = false);
}
