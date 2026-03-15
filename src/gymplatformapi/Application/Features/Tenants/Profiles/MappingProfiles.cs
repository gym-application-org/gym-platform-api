using Application.Features.Tenants.Commands.Create;
using Application.Features.Tenants.Commands.Delete;
using Application.Features.Tenants.Commands.Update;
using Application.Features.Tenants.Queries.GetById;
using Application.Features.Tenants.Queries.GetList;
using Application.Features.Tenants.Queries.GetMy;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.Tenants.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Tenant, CreateWithOwnerTenantCommand>()
            .ForMember(dest => dest.TenantIsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.TenantSubdomain, opt => opt.MapFrom(src => src.Subdomain))
            .ForMember(dest => dest.TenantName, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
        CreateMap<Staff, CreateWithOwnerTenantCommand>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(opt => opt.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(opt => opt.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(opt => opt.Phone))
            .ReverseMap();
        CreateMap<User, CreateWithOwnerTenantCommand>().ForMember(dest => dest.Email, opt => opt.MapFrom(opt => opt.Email)).ReverseMap();
        CreateMap<Tenant, CreatedTenantWithOwnerResponse>().ReverseMap();
        CreateMap<Tenant, UpdateTenantCommand>().ReverseMap();
        CreateMap<Tenant, UpdatedTenantResponse>().ReverseMap();
        CreateMap<Tenant, DeleteTenantCommand>().ReverseMap();
        CreateMap<Tenant, DeletedTenantResponse>().ReverseMap();
        CreateMap<Tenant, GetByIdTenantResponse>().ReverseMap();
        CreateMap<Tenant, GetMyTenantResponse>().ReverseMap();
        CreateMap<Tenant, GetListTenantListItemDto>().ReverseMap();
        CreateMap<IPaginate<Tenant>, GetListResponse<GetListTenantListItemDto>>().ReverseMap();
    }
}
