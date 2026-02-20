using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MemberRepository : EfRepositoryBase<Member, Guid, BaseDbContext>, IMemberRepository
{
    public MemberRepository(BaseDbContext context)
        : base(context) { }
}
