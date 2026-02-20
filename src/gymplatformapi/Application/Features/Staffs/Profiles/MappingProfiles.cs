using Application.Features.Staffs.Commands.Create;
using Application.Features.Staffs.Commands.Delete;
using Application.Features.Staffs.Commands.Update;
using Application.Features.Staffs.Queries.GetById;
using Application.Features.Staffs.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Staffs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Staff, CreateStaffCommand>().ReverseMap();
        CreateMap<Staff, CreatedStaffResponse>().ReverseMap();
        CreateMap<Staff, UpdateStaffCommand>().ReverseMap();
        CreateMap<Staff, UpdatedStaffResponse>().ReverseMap();
        CreateMap<Staff, DeleteStaffCommand>().ReverseMap();
        CreateMap<Staff, DeletedStaffResponse>().ReverseMap();
        CreateMap<Staff, GetByIdStaffResponse>().ReverseMap();
        CreateMap<Staff, GetListStaffListItemDto>().ReverseMap();
        CreateMap<IPaginate<Staff>, GetListResponse<GetListStaffListItemDto>>().ReverseMap();
    }
}
