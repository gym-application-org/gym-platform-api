using Core.Application.Responses;

namespace Application.Features.Tenants.Commands.Update;

public class UpdatedTenantResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
