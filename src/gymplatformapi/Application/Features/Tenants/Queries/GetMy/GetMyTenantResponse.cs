using Core.Application.Responses;

namespace Application.Features.Tenants.Queries.GetMy;

public class GetMyTenantResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
