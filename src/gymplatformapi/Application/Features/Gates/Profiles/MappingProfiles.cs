using Application.Features.Gates.Commands.Create;
using Application.Features.Gates.Commands.Delete;
using Application.Features.Gates.Commands.Update;
using Application.Features.Gates.Queries.GetById;
using Application.Features.Gates.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Gates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Gate, CreateGateCommand>().ReverseMap();
        CreateMap<Gate, CreatedGateResponse>().ReverseMap();
        CreateMap<Gate, UpdateGateCommand>().ReverseMap();
        CreateMap<Gate, UpdatedGateResponse>().ReverseMap();
        CreateMap<Gate, DeleteGateCommand>().ReverseMap();
        CreateMap<Gate, DeletedGateResponse>().ReverseMap();
        CreateMap<Gate, GetByIdGateResponse>().ReverseMap();
        CreateMap<Gate, GetListGateListItemDto>().ReverseMap();
        CreateMap<IPaginate<Gate>, GetListResponse<GetListGateListItemDto>>().ReverseMap();
    }
}
