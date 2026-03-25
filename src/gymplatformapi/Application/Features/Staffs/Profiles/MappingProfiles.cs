using Application.Features.Staffs.Commands.Create;
using Application.Features.Staffs.Commands.Delete;
using Application.Features.Staffs.Commands.Update;
using Application.Features.Staffs.Queries.GetById;
using Application.Features.Staffs.Queries.GetByIdAdmin;
using Application.Features.Staffs.Queries.GetList;
using Application.Features.Staffs.Queries.GetListAdmin;
using Application.Features.Tenants.Commands.Create;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.Staffs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Staff, CreateStaffCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(opt => opt.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(opt => opt.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(opt => opt.Phone))
            .ReverseMap();
        CreateMap<User, CreateStaffCommand>().ForMember(dest => dest.Email, opt => opt.MapFrom(opt => opt.Email)).ReverseMap();
        CreateMap<Staff, CreatedStaffResponse>().ReverseMap();
        CreateMap<Staff, UpdateStaffCommand>().ReverseMap();
        CreateMap<Staff, UpdatedStaffResponse>().ReverseMap();
        CreateMap<Staff, DeleteStaffCommand>().ReverseMap();
        CreateMap<Staff, DeletedStaffResponse>().ReverseMap();
        CreateMap<Staff, GetByIdStaffResponse>().ReverseMap();
        CreateMap<Staff, GetListStaffListItemDto>().ReverseMap();
        CreateMap<IPaginate<Staff>, GetListResponse<GetListStaffListItemDto>>().ReverseMap();
        CreateMap<Staff, GetByIdAdminStaffResponse>().ReverseMap();
        CreateMap<Staff, GetListAdminStaffListItemDto>().ReverseMap();
        CreateMap<IPaginate<Staff>, GetListResponse<GetListAdminStaffListItemDto>>().ReverseMap();
    }
}
