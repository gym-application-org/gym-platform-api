using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.Members.Queries.GetListAdmin;

public class GetListAdminMemberListItemDto : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public MemberStatus Status { get; set; }
    public int UserId { get; set; }
}
