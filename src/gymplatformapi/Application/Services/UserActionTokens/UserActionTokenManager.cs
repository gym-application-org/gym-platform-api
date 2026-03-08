using System.Linq.Expressions;
using Application.Features.UserActionTokens.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.UserActionTokens;

public class UserActionTokenManager : IUserActionTokenService
{
    private readonly IUserActionTokenRepository _userActionTokenRepository;
    private readonly UserActionTokenBusinessRules _userActionTokenBusinessRules;

    public UserActionTokenManager(
        IUserActionTokenRepository userActionTokenRepository,
        UserActionTokenBusinessRules userActionTokenBusinessRules
    )
    {
        _userActionTokenRepository = userActionTokenRepository;
        _userActionTokenBusinessRules = userActionTokenBusinessRules;
    }

    public async Task<UserActionToken?> GetAsync(
        Expression<Func<UserActionToken, bool>> predicate,
        Func<IQueryable<UserActionToken>, IIncludableQueryable<UserActionToken, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        UserActionToken? userActionToken = await _userActionTokenRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userActionToken;
    }

    public async Task<IPaginate<UserActionToken>?> GetListAsync(
        Expression<Func<UserActionToken, bool>>? predicate = null,
        Func<IQueryable<UserActionToken>, IOrderedQueryable<UserActionToken>>? orderBy = null,
        Func<IQueryable<UserActionToken>, IIncludableQueryable<UserActionToken, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<UserActionToken> userActionTokenList = await _userActionTokenRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userActionTokenList;
    }

    public async Task<UserActionToken> AddAsync(UserActionToken userActionToken)
    {
        UserActionToken addedUserActionToken = await _userActionTokenRepository.AddAsync(userActionToken);

        return addedUserActionToken;
    }

    public async Task<UserActionToken> UpdateAsync(UserActionToken userActionToken)
    {
        UserActionToken updatedUserActionToken = await _userActionTokenRepository.UpdateAsync(userActionToken);

        return updatedUserActionToken;
    }

    public async Task<UserActionToken> DeleteAsync(UserActionToken userActionToken, bool permanent = false)
    {
        UserActionToken deletedUserActionToken = await _userActionTokenRepository.DeleteAsync(userActionToken);

        return deletedUserActionToken;
    }
}
