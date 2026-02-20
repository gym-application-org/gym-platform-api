using Core.Application.Dtos;

namespace Application.Features.Tenants.Queries.GetList;

public class GetListTenantListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
