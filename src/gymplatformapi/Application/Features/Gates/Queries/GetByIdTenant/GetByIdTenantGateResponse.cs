using Core.Application.Responses;

namespace Application.Features.Gates.Queries.GetByIdTenant;

public class GetByIdTenantGateResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GateCode { get; set; }
    public bool IsActive { get; set; }
}
