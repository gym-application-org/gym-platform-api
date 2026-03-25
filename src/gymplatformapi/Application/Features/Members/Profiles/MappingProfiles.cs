using Application.Features.Members.Commands.Create;
using Application.Features.Members.Commands.Delete;
using Application.Features.Members.Commands.Update;
using Application.Features.Members.Queries.GetById;
using Application.Features.Members.Queries.GetByIdAdmin;
using Application.Features.Members.Queries.GetList;
using Application.Features.Members.Queries.GetListAdmin;
using Application.Features.Staffs.Commands.Create;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.Members.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Member, CreateMemberCommand>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(opt => opt.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(opt => opt.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(opt => opt.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(opt => opt.Phone))
            .ReverseMap();
        CreateMap<User, CreateMemberCommand>().ForMember(dest => dest.Email, opt => opt.MapFrom(opt => opt.Email)).ReverseMap();
        CreateMap<Member, CreatedMemberResponse>().ReverseMap();
        CreateMap<Member, UpdateMemberCommand>().ReverseMap();
        CreateMap<Member, UpdatedMemberResponse>().ReverseMap();
        CreateMap<Member, DeleteMemberCommand>().ReverseMap();
        CreateMap<Member, DeletedMemberResponse>().ReverseMap();
        CreateMap<Member, GetByIdMemberResponse>().ReverseMap();
        CreateMap<Member, GetListMemberListItemDto>().ReverseMap();
        CreateMap<IPaginate<Member>, GetListResponse<GetListMemberListItemDto>>().ReverseMap();
        CreateMap<Member, GetByIdAdminMemberResponse>().ReverseMap();
        CreateMap<Member, GetListAdminMemberListItemDto>().ReverseMap();
        CreateMap<IPaginate<Member>, GetListResponse<GetListAdminMemberListItemDto>>().ReverseMap();
    }
}
