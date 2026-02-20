using Core.Application.Responses;

namespace Application.Features.Tenants.Queries.GetById;

public class GetByIdTenantResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
