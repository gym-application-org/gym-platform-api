using Core.Application.Responses;

namespace Application.Features.Gates.Queries.GetByIdAdmin;

public class GetByIdGateResponse : IResponse
{
    public int Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string GateCode { get; set; }
    public bool IsActive { get; set; }
}
