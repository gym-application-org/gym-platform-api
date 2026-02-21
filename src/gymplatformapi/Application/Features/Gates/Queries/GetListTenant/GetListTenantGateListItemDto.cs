using Core.Application.Dtos;

namespace Application.Features.Gates.Queries.GetListTenant;

public class GetListTenantGateListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GateCode { get; set; }
    public bool IsActive { get; set; }
}
