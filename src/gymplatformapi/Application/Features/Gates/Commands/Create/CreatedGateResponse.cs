using Core.Application.Responses;

namespace Application.Features.Gates.Commands.Create;

public class CreatedGateResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GateCode { get; set; }
    public bool IsActive { get; set; }
}
