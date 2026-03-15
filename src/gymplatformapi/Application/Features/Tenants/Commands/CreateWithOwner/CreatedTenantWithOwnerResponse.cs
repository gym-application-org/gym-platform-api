using Core.Application.Responses;

namespace Application.Features.Tenants.Commands.Create;

public class CreatedTenantWithOwnerResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
