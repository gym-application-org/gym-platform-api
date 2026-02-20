using Core.Application.Responses;

namespace Application.Features.Tenants.Commands.Create;

public class CreatedTenantResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
