using Core.Application.Dtos;

namespace Application.Features.Gates.Queries.GetList;

public class GetListGateListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GateCode { get; set; }
    public bool IsActive { get; set; }
}
