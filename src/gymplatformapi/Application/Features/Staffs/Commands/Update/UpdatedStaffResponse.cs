using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Staffs.Commands.Update;

public class UpdatedStaffResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public StaffRole Role { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }
}
