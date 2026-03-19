using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.UserActionTokens;

public interface IUserActionTokenService
{
    Task<UserActionToken?> GetAsync(
        Expression<Func<UserActionToken, bool>> predicate,
        Func<IQueryable<UserActionToken>, IIncludableQueryable<UserActionToken, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<UserActionToken>?> GetListAsync(
        Expression<Func<UserActionToken, bool>>? predicate = null,
        Func<IQueryable<UserActionToken>, IOrderedQueryable<UserActionToken>>? orderBy = null,
        Func<IQueryable<UserActionToken>, IIncludableQueryable<UserActionToken, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<UserActionToken> AddAsync(UserActionToken userActionToken);
    Task<UserActionToken> UpdateAsync(UserActionToken userActionToken);
    Task<UserActionToken> DeleteAsync(UserActionToken userActionToken, bool permanent = false);
}
