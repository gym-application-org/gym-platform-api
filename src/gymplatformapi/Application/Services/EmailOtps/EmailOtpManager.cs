using System.Linq.Expressions;
using Application.Features.EmailOtps.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.EmailOtps;

public class EmailOtpManager : IEmailOtpService
{
    private readonly IEmailOtpRepository _emailOtpRepository;
    private readonly EmailOtpBusinessRules _emailOtpBusinessRules;

    public EmailOtpManager(IEmailOtpRepository emailOtpRepository, EmailOtpBusinessRules emailOtpBusinessRules)
    {
        _emailOtpRepository = emailOtpRepository;
        _emailOtpBusinessRules = emailOtpBusinessRules;
    }

    public async Task<EmailOtp?> GetAsync(
        Expression<Func<EmailOtp, bool>> predicate,
        Func<IQueryable<EmailOtp>, IIncludableQueryable<EmailOtp, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        EmailOtp? emailOtp = await _emailOtpRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return emailOtp;
    }

    public async Task<IPaginate<EmailOtp>?> GetListAsync(
        Expression<Func<EmailOtp, bool>>? predicate = null,
        Func<IQueryable<EmailOtp>, IOrderedQueryable<EmailOtp>>? orderBy = null,
        Func<IQueryable<EmailOtp>, IIncludableQueryable<EmailOtp, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<EmailOtp> emailOtpList = await _emailOtpRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return emailOtpList;
    }

    public async Task<EmailOtp> AddAsync(EmailOtp emailOtp)
    {
        EmailOtp addedEmailOtp = await _emailOtpRepository.AddAsync(emailOtp);

        return addedEmailOtp;
    }

    public async Task<EmailOtp> UpdateAsync(EmailOtp emailOtp)
    {
        EmailOtp updatedEmailOtp = await _emailOtpRepository.UpdateAsync(emailOtp);

        return updatedEmailOtp;
    }

    public async Task<EmailOtp> DeleteAsync(EmailOtp emailOtp, bool permanent = false)
    {
        EmailOtp deletedEmailOtp = await _emailOtpRepository.DeleteAsync(emailOtp);

        return deletedEmailOtp;
    }
}
