using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.Staffs.Queries.GetList;

public class GetListStaffListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public StaffRole Role { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }
}
