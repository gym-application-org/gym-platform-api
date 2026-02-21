using Core.Application.Dtos;

namespace Application.Features.Gates.Queries.GetListAdmin;

public class GetListGateListItemDto : IDto
{
    public int Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string GateCode { get; set; }
    public bool IsActive { get; set; }
}
