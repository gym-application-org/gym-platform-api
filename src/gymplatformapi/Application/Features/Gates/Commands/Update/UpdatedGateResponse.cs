using Core.Application.Responses;

namespace Application.Features.Gates.Commands.Update;

public class UpdatedGateResponse : IResponse
{
    public int Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string GateCode { get; set; }
    public bool IsActive { get; set; }
}
